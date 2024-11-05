using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public IEventScreen EventScreen { get; private set; }
    
    public GameObject Player { get; private set; }

    // Evento para notificar quando o Player for configurado
    public event System.Action OnPlayerSet;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //EventScreen = EventScreen.Instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayer(GameObject player)
    {
        Player = player;
        OnPlayerSet?.Invoke(); // Dispara o evento quando o Player for configurado
    }
}
