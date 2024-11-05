using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    void Update()
    {
        // Verifica se o jogador pressionou a tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Abre o GameMenu quando a tecla for pressionada
           // MenuManager.Instance.OpenGameMenu();
        }
    }
}
