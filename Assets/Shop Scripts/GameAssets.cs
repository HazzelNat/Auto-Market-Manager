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

    // Existing shop item sprites
    [Header("Shop Item Sprites")]
    [SerializeField] private Sprite egg_S;
    [SerializeField] private Sprite bottled_Water_S;
    [SerializeField] private Sprite bread_S;
    [SerializeField] private Sprite rice_S;
    [SerializeField] private Sprite snacks_S;
    [SerializeField] private Sprite instant_Noodles_S;

    // New section for world-space item sprites
    [Header("World Item Sprites")]
    [SerializeField] private Sprite defaultWorldItemSprite; // A generic fallback sprite
    [SerializeField] private Sprite worldEggSprite;
    [SerializeField] private Sprite worldBottledWaterSprite;
    [SerializeField] private Sprite worldBreadSprite;
    [SerializeField] private Sprite worldRiceSprite;
    [SerializeField] private Sprite worldSnacksSprite;
    [SerializeField] private Sprite worldInstantNoodlesSprite;

    // Existing shop sprite properties
    public Sprite Egg_S => egg_S;
    public Sprite Bottled_Water_S => bottled_Water_S;
    public Sprite Bread_S => bread_S;
    public Sprite Rice_S => rice_S;
    public Sprite Snacks_S => snacks_S;
    public Sprite Instant_Noodles_S => instant_Noodles_S;

    // New world sprite method
    public Sprite GetWorldItemSprite(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Eggs:
                return worldEggSprite != null ? worldEggSprite : defaultWorldItemSprite;
            case Item.ItemType.Bottled_Water:
                return worldBottledWaterSprite != null ? worldBottledWaterSprite : defaultWorldItemSprite;
            case Item.ItemType.Bread:
                return worldBreadSprite != null ? worldBreadSprite : defaultWorldItemSprite;
            case Item.ItemType.Rice:
                return worldRiceSprite != null ? worldRiceSprite : defaultWorldItemSprite;
            case Item.ItemType.Snacks:
                return worldSnacksSprite != null ? worldSnacksSprite : defaultWorldItemSprite;
            case Item.ItemType.Instant_Noodles:
                return worldInstantNoodlesSprite != null ? worldInstantNoodlesSprite : defaultWorldItemSprite;
            default:
                return defaultWorldItemSprite;
        }
    }

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