using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    public int amount = 1;

    public void PickUp(Inventory inventory)
    {
        if (inventory.AddItem(item, amount))
        {
            Destroy(gameObject);
        }

        inventory.onInventoryChanged?.Invoke();
    }
}