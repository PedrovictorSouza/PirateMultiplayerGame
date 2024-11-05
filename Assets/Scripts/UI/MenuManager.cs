using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameMenu gameMenuPrefab;
    public MainMenu mainMenuPrefab;

    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    private HashSet<GameObject> activeMenus = new HashSet<GameObject>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        if (mainMenuPrefab != null)
        {
            MainMenu mainMenuInstance = Instantiate(mainMenuPrefab);
            ShowMenu(mainMenuInstance.gameObject);
        }
        else
        {
            Debug.LogError("MainMenu prefab não atribuído no MenuManager.");
        }
    }

    public void StartHost()
    {
        CloseAllMenus();
        OpenGameMenu();
    }

    public void StartClient()
    {
        CloseAllMenus();
        OpenGameMenu();
    }

    private void OpenGameMenu()
    {
        if (gameMenuPrefab != null)
        {
            GameMenu gameMenuInstance = Instantiate(gameMenuPrefab);
            ShowMenu(gameMenuInstance.gameObject);
        }
        else
        {
            Debug.LogError("GameMenu prefab não atribuído no MenuManager.");
        }
    }

    private void ShowMenu(GameObject menu)
    {
        CloseAllMenus(); // Fecha todos os menus antes de abrir um novo
        activeMenus.Add(menu);
        menu.SetActive(true);
    }

    private void CloseAllMenus()
    {
        foreach (GameObject menu in activeMenus)
        {
            Destroy(menu); // Destroi as instâncias de menu
        }
        activeMenus.Clear();
    }
}
