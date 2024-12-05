using UnityEngine;

public class GameAssetsHandler : MonoBehaviour
{
    private static GameAssetsHandler _instance;
    
    public static GameAssetsHandler instance {
        get {
            if (_instance == null) {
                // Find existing instance in scene first
                _instance = FindObjectOfType<GameAssetsHandler>();
                
                // If no instance exists, create one
                if (_instance == null) {
                    GameObject gameAssetsObj = new GameObject("GameAssetsHandler");
                    _instance = gameAssetsObj.AddComponent<GameAssetsHandler>();
                }
            }
            return _instance;
        }
    }

    // Reference these in the Unity Inspector
    [Header("Item Sprites")]
    [SerializeField] private Sprite egg_S;
    [SerializeField] private Sprite bottled_Water_S;
    [SerializeField] private Sprite bread_S;
    [SerializeField] private Sprite rice_S;
    [SerializeField] private Sprite snacks_S;
    [SerializeField] private Sprite instant_Noodles_S;

    // Public properties to access sprites
    public Sprite Egg_S => egg_S;
    public Sprite Bottled_Water_S => bottled_Water_S;
    public Sprite Bread_S => bread_S;
    public Sprite Rice_S => rice_S;
    public Sprite Snacks_S => snacks_S;
    public Sprite Instant_Noodles_S => instant_Noodles_S;

    private void Awake()
    {
        // Ensure only one instance exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
