using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ShipNetworkHandler
{
    private readonly Ship ship;

    public ShipNetworkHandler(Ship shipInstance)
    {
        ship = shipInstance;
    }

    public void RequestMove(Vector3 position)
    {
        SetTargetPositionServerRpc(position);
    }

    public void RequestDestroy(Ship targetShip)
    {
        if (targetShip == null) return;
        ChangeSpriteServerRpc(targetShip.GetComponent<NetworkObject>().NetworkObjectId);

    }

    [ServerRpc]
    void ChangeSpriteServerRpc(ulong shipId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(shipId, out NetworkObject networkObject))
        {
            ChangeSpriteClientRpc(networkObject.NetworkObjectId);
        }
    }

    [ClientRpc]
    void ChangeSpriteClientRpc(ulong shipId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(shipId, out NetworkObject networkObject))
        {
            Ship targetShip = networkObject.GetComponent<Ship>();
            if (targetShip != null)
            {
                targetShip.ChangeSprite(); // Chama o método para alterar o sprite
            }
        }
    }

    [ServerRpc]
    void DestroyShipServerRpc(ulong shipId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(shipId, out NetworkObject networkObject))
        {
            networkObject.Despawn(); // Garante a remoção do objeto de forma sincronizada
            GameObject.Destroy(networkObject.gameObject); // Destroi o GameObject
        }
    }

    [ServerRpc]
    private void SetTargetPositionServerRpc(Vector3 worldPosition)
    {
        ship.UpdatePosition(worldPosition);
        
        Vector3 direction = (worldPosition - ship.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        UpdateRotationClientRpc(angle);
    }

    [ClientRpc]
    private void UpdateRotationClientRpc(float angle)
    {
        ship.UpdateRotation(angle - 90);
    }
}
