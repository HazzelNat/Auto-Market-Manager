using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab; // The prefab to spawn
    [SerializeField] private Vector2Int gridSize = new Vector2Int(16, 16);

    public void SpawnItem(Vector2 gridPosition)
    {
        Vector2 worldPosition = new Vector2(gridPosition.x * gridSize.x, gridPosition.y * gridSize.y);
        Instantiate(itemPrefab, worldPosition, Quaternion.identity);
    }
}
