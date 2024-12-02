// using UnityEngine;
// using System.Collections.Generic;

// public class InventoryItemSpawner : MonoBehaviour
// {
//     [SerializeField] private PlayerInventory playerInventory;
    
//     [Header("Grid Spawning Settings")]
//     [SerializeField] private int gridWidth = 16;
//     [SerializeField] private int gridHeight = 16;
//     [SerializeField] private float cellSize = 1f;
//     [SerializeField] private Vector2 gridOrigin = Vector2.zero;

//     [Header("Item Appearance")]
//     [SerializeField] private float itemScale = 0.8f; // Scale relative to cell size

//     // Track spawned items in the grid
//     private GameObject[,] gridItems;

//     private void Awake()
//     {
//         // Initialize grid tracking
//         gridItems = new GameObject[gridWidth, gridHeight];

//         // If no PlayerInventory is set, try to get it from the same GameObject
//         if (playerInventory == null)
//         {
//             playerInventory = GetComponent<PlayerInventory>();
//         }
//     }

//     public void SpawnItemInGrid(Item.ItemType itemType)
//     {
//         // Find the first empty grid cell
//         Vector2Int? emptyCell = FindEmptyGridCell();
        
//         if (!emptyCell.HasValue)
//         {
//             Debug.LogWarning("No empty grid cell available!");
//             return;
//         }

//         // Create the item GameObject
//         GameObject spawnedItem = CreateItemGameObject(itemType);
        
//         // Calculate world position for the grid cell
//         Vector3 spawnPosition = GetWorldPositionFromGridCell(emptyCell.Value);
//         spawnedItem.transform.position = spawnPosition;

//         // Store the item in the grid
//         gridItems[emptyCell.Value.x, emptyCell.Value.y] = spawnedItem;
//     }

//     private GameObject CreateItemGameObject(Item.ItemType itemType)
//     {
//         // Create a new GameObject
//         GameObject itemObject = new GameObject(itemType.ToString() + "_Item");
        
//         // Add SpriteRenderer component
//         SpriteRenderer spriteRenderer = itemObject.AddComponent<SpriteRenderer>();
        
//         // Set sprite using existing GetSprite method
//         spriteRenderer.sprite = Item.GetSprite(itemType);

//         // Set sorting order to ensure visibility
//         spriteRenderer.sortingOrder = 10;

//         // Scale the item
//         itemObject.transform.localScale = Vector3.one * cellSize * itemScale;

//         return itemObject;
//     }

//     private Vector2Int? FindEmptyGridCell()
//     {
//         for (int x = 0; x < gridWidth; x++)
//         {
//             for (int y = 0; y < gridHeight; y++)
//             {
//                 if (gridItems[x, y] == null)
//                 {
//                     return new Vector2Int(x, y);
//                 }
//             }
//         }
//         return null;
//     }

//     private Vector3 GetWorldPositionFromGridCell(Vector2Int cell)
//     {
//         return new Vector3(
//             gridOrigin.x + cell.x * cellSize,
//             gridOrigin.y + cell.y * cellSize,
//             0
//         );
//     }

//     // Method to clear a specific grid cell
//     public void ClearGridCell(int x, int y)
//     {
//         if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
//         {
//             if (gridItems[x, y] != null)
//             {
//                 Destroy(gridItems[x, y]);
//                 gridItems[x, y] = null;
//             }
//         }
//     }

//     // Method to manually spawn an item at a specific grid position
//     public void SpawnItemAtGridPosition(Item.ItemType itemType, int x, int y)
//     {
//         if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
//         {
//             Debug.LogWarning("Invalid grid position!");
//             return;
//         }

//         if (gridItems[x, y] != null)
//         {
//             Debug.LogWarning("Grid cell is already occupied!");
//             return;
//         }

//         GameObject spawnedItem = CreateItemGameObject(itemType);
//         Vector3 spawnPosition = GetWorldPositionFromGridCell(new Vector2Int(x, y));
//         spawnedItem.transform.position = spawnPosition;

//         gridItems[x, y] = spawnedItem;
//     }

//     // Optional: Visualize the grid in Scene view
//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.yellow;
//         for (int x = 0; x < gridWidth; x++)
//         {
//             for (int y = 0; y < gridHeight; y++)
//             {
//                 Vector3 cellCenter = GetWorldPositionFromGridCell(new Vector2Int(x, y));
//                 Gizmos.DrawWireCube(cellCenter, Vector3.one * cellSize);
//             }
//         }
//     }
// }
using UnityEngine;
using System.Collections.Generic;

public class InventoryItemSpawner : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Grid Spawning Settings")]
    [SerializeField] private int gridWidth = 16;
    [SerializeField] private int gridHeight = 16;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Vector2 gridOrigin = Vector2.zero;

    [Header("Item Appearance")]
    [SerializeField] private float itemScale = 0.8f;

    private GameObject[,] gridItems;

    private void Awake()
    {
        gridItems = new GameObject[gridWidth, gridHeight];

        if (playerInventory == null)
        {
            playerInventory = GetComponent<PlayerInventory>();
        }

        Debug.Log($"Spawner initialized. Grid: {gridWidth}x{gridHeight}, Cell Size: {cellSize}, Origin: {gridOrigin}");
    }

    public void SpawnItemInGrid(Item.ItemType itemType)
    {
        Vector2Int? emptyCell = FindEmptyGridCell();

        if (!emptyCell.HasValue)
        {
            Debug.LogWarning("No empty grid cell available!");
            return;
        }

        GameObject spawnedItem = CreateItemGameObject(itemType);

        Vector3 spawnPosition = GetWorldPositionFromGridCell(emptyCell.Value);
        Debug.Log($"Spawning {itemType} at grid cell {emptyCell.Value}, world position: {spawnPosition}");

        spawnedItem.transform.position = spawnPosition;
        gridItems[emptyCell.Value.x, emptyCell.Value.y] = spawnedItem;
    }

    private GameObject CreateItemGameObject(Item.ItemType itemType)
    {
        GameObject itemObject = new GameObject(itemType.ToString() + "_Item");

        SpriteRenderer spriteRenderer = itemObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Item.GetSprite(itemType);

        if (spriteRenderer.sprite == null)
        {
            Debug.LogError($"Sprite for {itemType} is null!");
        }

        spriteRenderer.sortingOrder = 10;
        itemObject.transform.localScale = Vector3.one * cellSize * itemScale;

        return itemObject;
    }

    private Vector2Int? FindEmptyGridCell()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (gridItems[x, y] == null)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return null;
    }

    private Vector3 GetWorldPositionFromGridCell(Vector2Int cell)
    {
        Vector3 spawnPosition = new Vector3(
            gridOrigin.x + cell.x * cellSize,
            gridOrigin.y + cell.y * cellSize,
            0
        );

        Debug.Log($"Grid cell {cell} corresponds to world position {spawnPosition}");
        return spawnPosition;
    }
}
