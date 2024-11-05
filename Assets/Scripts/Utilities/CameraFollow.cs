using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    void OnEnable()
    {
        // Inscreve-se no evento quando o Player for configurado
        GameManager.Instance.OnPlayerSet += SetPlayer;
    }

    void OnDisable()
    {
        // Remove a inscrição no evento quando o objeto for desativado
        GameManager.Instance.OnPlayerSet -= SetPlayer;
    }

    void SetPlayer()
    {
        player = GameManager.Instance.Player.transform; // Agora, garantimos que o Player foi configurado
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
