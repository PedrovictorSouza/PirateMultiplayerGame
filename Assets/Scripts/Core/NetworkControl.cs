using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkControl : MonoBehaviour
{
    private NetworkManager networkManager;

    void Awake()
    {
        // Encontre o NetworkManager no jogo
        networkManager = NetworkManager.Singleton;
    }

    public void StartHost()
    {
        if (networkManager != null)
        {
            networkManager.StartHost();
        }
        else
        {
            Debug.LogError("NetworkManager not found!");
        }
    }

    public void StartClient()
    {
        if (networkManager != null)
        {
            // Defina o endereço do servidor aqui
            var transport = networkManager.NetworkConfig.NetworkTransport as UnityTransport;
            if (transport != null)
            {
                transport.SetConnectionData("127.0.0.1", 9999);
                networkManager.StartClient();
            }
            else
            {
                Debug.LogError("UnityTransport component not found!");
            }
        }
        else
        {
            Debug.LogError("NetworkManager not found!");
        }
    }
}
