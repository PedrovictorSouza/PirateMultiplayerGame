using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    private void Awake() {
        if (serverButton != null) {
            serverButton.onClick.AddListener(() => {
                Debug.Log("Clicked Server Button.");
                if (NetworkManager.Singleton == null) {
                    Debug.LogError("NetworkManager.Singleton is null!");
                } else {
                    NetworkManager.Singleton.StartServer();
                    Debug.Log("Server started.");
                    MenuManager.Instance.StartHost();  // Fechar MainMenu e abrir GameMenu
                }
            });
        }

        if (hostButton != null) {
            hostButton.onClick.AddListener(() => {
                Debug.Log("Clicked Host Button.");
                if (NetworkManager.Singleton == null) {
                    Debug.LogError("NetworkManager.Singleton is null!");
                } else {
                    NetworkManager.Singleton.StartHost();
                    Debug.Log("Host started.");
                    MenuManager.Instance.StartHost();  // Fechar MainMenu e abrir GameMenu
                }
            });
        }

        if (clientButton != null) {
            clientButton.onClick.AddListener(() => {
                Debug.Log("Clicked Client Button.");
                if (NetworkManager.Singleton == null) {
                    Debug.LogError("NetworkManager.Singleton is null!");
                } else {
                    NetworkManager.Singleton.StartClient();
                    Debug.Log("Client started.");
                    MenuManager.Instance.StartClient();  // Fechar MainMenu e abrir GameMenu
                }
            });
        }
    }
}
