public class EventScreen : MonoBehaviour, IEventScreen
{
    public static EventScreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayMessage(string message) { /* ... */ }
    public void Hide() { /* ... */ }
}
