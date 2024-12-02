using UnityEngine;
using System.Collections;

public class InventoryWorldDisplay : MonoBehaviour
{
    [SerializeField] private GameObject itemPickupPrefab;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float verticalOffset = 1f;
    [SerializeField] private float fadeOutTime = 0.5f;

    private PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();

        // If no prefab is assigned, create a default one
        if (itemPickupPrefab == null)
        {
            itemPickupPrefab = new GameObject("ItemPickupSprite");
            SpriteRenderer spriteRenderer = itemPickupPrefab.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 10; // Ensure it's drawn on top of other sprites
        }
    }

    public void ShowItemPickupWorldSprite(Item.ItemType itemType)
    {
        // Instantiate the pickup sprite above the player
        GameObject pickupSprite = Instantiate(
            itemPickupPrefab, 
            transform.position + Vector3.up * verticalOffset, 
            Quaternion.identity
        );

        // Set the sprite for the item
        SpriteRenderer spriteRenderer = pickupSprite.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = Item.GetWorldSprite(itemType);
        }

        // Start coroutine to handle sprite display and fade out
        StartCoroutine(HandleItemPickupDisplay(pickupSprite));
    }

    private IEnumerator HandleItemPickupDisplay(GameObject pickupSprite)
    {
        SpriteRenderer spriteRenderer = pickupSprite.GetComponent<SpriteRenderer>();

        // Wait for initial display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // Destroy the pickup sprite
        Destroy(pickupSprite);
    }
}
