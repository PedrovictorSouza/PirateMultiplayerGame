using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 spawnCenter = new Vector3(0, 0, 0);
    [SerializeField] private float spawnRadius = 10f; 
    [SerializeField] private Sprite damagedSprite;

    private ShipNetworkHandler networkHandler;
    private ShipMovementSystem movementSystem;
    private ShipInputSystem inputSystem;
    private ShipRotationSystem rotationSystem;
    private ShipAttackSystem attackSystem;
    private IEventScreen eventScreen;
    private SpriteRenderer spriteRenderer;

    private PlayerInput playerInput;

    private void Awake()
    {
        InitializeSystems();
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            playerInput = gameObject.AddComponent<PlayerInput>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer não encontrado!");
        }
    }

    private void InitializeSystems()
    {
        networkHandler = new ShipNetworkHandler(this);
        movementSystem = new ShipMovementSystem(transform, speed);
        rotationSystem = new ShipRotationSystem(transform);
        inputSystem = new ShipInputSystem(Camera.main, this);
        eventScreen = GameManager.Instance.EventScreen;
        attackSystem = new ShipAttackSystem(transform);
    }

    void Start()
    {
        if (IsOwner)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            transform.position = spawnPoint;
            Debug.Log("IsOwner");
            GameManager.Instance.SetPlayer(this.gameObject);
            gameObject.SetActive(true);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        return spawnCenter + new Vector3(randomPoint.x, 0, randomPoint.y);
    }

    void Update()
    {
        if (!IsOwner) return;

        if (movementSystem.IsMoving)
        {
            movementSystem.UpdateMovement();
        }
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (!IsOwner || !context.performed) return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Ship targetShip = hit.collider.GetComponent<Ship>();
            if (targetShip != null && targetShip != this)
            {
                Debug.Log("Navio detectado: " + targetShip.name);
                ShowEventScreen("Combate iniciado!");
                attackSystem.ExecuteAttack(targetShip); // Executa o ataque
                StartCoroutine(HideEventScreenAfterDelay(3f));
            }
            else
            {
                Vector3 targetPosition = inputSystem.GetWorldPositionFromMouse();
                networkHandler.RequestMove(targetPosition);
            }
        }
        else
        {
            Vector3 targetPosition = inputSystem.GetWorldPositionFromMouse();
            networkHandler.RequestMove(targetPosition);
        }
    }

    private void ShowEventScreen(string message)
    {
        eventScreen.DisplayMessage(message);
    }

    private IEnumerator HideEventScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        eventScreen.Hide();
    }

    public void ChangeSprite()
    {
        if (spriteRenderer != null && damagedSprite != null)
        {
            spriteRenderer.sprite = damagedSprite;
        }
    }

    public void UpdatePosition(Vector3 newPosition) => movementSystem.MoveTo(newPosition);
    public void UpdateRotation(float angle) => rotationSystem.RotateTo(angle);
}
