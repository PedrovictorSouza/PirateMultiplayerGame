using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform arrow; // Referência para o objeto da seta como RectTransform
    [SerializeField]
    private RectTransform canvasRectTransform; // O RectTransform do Canvas para manter a seta dentro da tela

    private IIslandProvider islandProvider;

    void Start()
    {
        islandProvider = FindObjectOfType<DrawCircles>() as IIslandProvider;

        if (islandProvider == null)
        {
            Debug.LogError("IIslandProvider não encontrado!");
            return;
        }

        if (canvasRectTransform == null)
        {
            canvasRectTransform = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (islandProvider != null)
        {
            // Considerando que a ilha mais próxima seja o alvo
            Coords closestIsland = islandProvider.GetClosestIsland(Vector3.zero); // Consideramos o centro do mundo

            if (closestIsland != null)
            {
                // Calcula a direção da posição atual até a ilha mais próxima
                Vector3 directionToIsland = closestIsland.ToVector3().normalized;

                // Converte para ângulo em graus
                float angle = Mathf.Atan2(directionToIsland.y, directionToIsland.x) * Mathf.Rad2Deg;

                // Ajusta a rotação da seta para apontar na direção correta
                arrow.rotation = Quaternion.Euler(0, 0, angle);

                // Posiciona a seta perto do canto da tela, mas sem encostar
                PositionArrowNearEdge(directionToIsland);
            }
        }
    }

    private void PositionArrowNearEdge(Vector3 direction)
    {
        float margin = 50f; // Margem de segurança para não encostar no canto
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        // Posição da seta baseada na direção calculada
        Vector2 arrowPosition = new Vector2(direction.x, direction.y).normalized * (canvasSize.y / 2 - margin);

        // Clampa a posição da seta para garantir que ela fique dentro da tela, mas próxima do limite
        arrowPosition.x = Mathf.Clamp(arrowPosition.x, -canvasSize.x / 2 + margin, canvasSize.x / 2 - margin);
        arrowPosition.y = Mathf.Clamp(arrowPosition.y, -canvasSize.y / 2 + margin, canvasSize.y / 2 - margin);

        // Atualiza a posição da seta
        arrow.anchoredPosition = arrowPosition;
    }
}
