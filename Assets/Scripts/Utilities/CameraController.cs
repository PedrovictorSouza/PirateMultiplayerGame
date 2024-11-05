using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 20.0f;
    public float edgeBorderThickness = 10.0f;
    public float maxMoveOffset = 50.0f; // Quão longe a câmera pode se mover a partir de sua posição inicial

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position; // Armazenar posição inicial
    }

    void Update()
    {
        Vector3 cameraMove = Vector3.zero;

        if (Input.mousePosition.x > Screen.width - edgeBorderThickness)
            cameraMove.x += moveSpeed * Time.deltaTime;
        if (Input.mousePosition.x < edgeBorderThickness)
            cameraMove.x -= moveSpeed * Time.deltaTime;
        if (Input.mousePosition.y > Screen.height - edgeBorderThickness)
            cameraMove.y += moveSpeed * Time.deltaTime;
        if (Input.mousePosition.y < edgeBorderThickness)
            cameraMove.y -= moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position + cameraMove;
        newPosition.x = Mathf.Clamp(newPosition.x, originalPosition.x - maxMoveOffset, originalPosition.x + maxMoveOffset);
        newPosition.y = Mathf.Clamp(newPosition.y, originalPosition.y - maxMoveOffset, originalPosition.y + maxMoveOffset);

        transform.position = newPosition;
    }
}
