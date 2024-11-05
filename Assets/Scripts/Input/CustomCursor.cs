using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Cursor padrão
    public Texture2D attackCursorTexture; // Cursor de ataque
    public Vector2 hotSpot = Vector2.zero; // Ponto de interação do cursor
    private bool isCustomCursorSet = false;
    private bool isAttackCursorActive = false;

    void Start()
    {
        Debug.Log("CustomCursor - Start: Inicializando o cursor customizado.");
        SetCustomCursor();
    }

    private void SetCustomCursor()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        isCustomCursorSet = true;
        isAttackCursorActive = false;
    }

    private void SetAttackCursor()
    {
        Cursor.SetCursor(attackCursorTexture, hotSpot, CursorMode.Auto);
        isAttackCursorActive = true;
    }

    void Update()
    {
        // Lança um Raycast da posição do mouse para detectar o objeto com a tag "Player"
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            if (!isAttackCursorActive)
            {
                SetAttackCursor();
            }
        }
        else
        {
            if (isAttackCursorActive)
            {
                SetCustomCursor();
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            isCustomCursorSet = false; // Força a redefinição do cursor ao ganhar o foco
            SetCustomCursor();
        }
        else
        {
            Debug.Log("CustomCursor - OnApplicationFocus: Perdeu o foco da aplicação.");
        }
    }

    void OnEnable()
    {
        SetCustomCursor();
    }

    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        isCustomCursorSet = false;
    }

    void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
