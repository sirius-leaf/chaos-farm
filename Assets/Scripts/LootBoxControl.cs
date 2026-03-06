using System.Collections.Generic;
using UnityEngine;

public class LootBoxControl : MonoBehaviour
{
    public List<LootItem> lootItems = new List<LootItem>();

    public void OpenBox(Inventory inventory)
    {
        foreach (var loot in lootItems)
        {
            inventory.AddItem(loot.item, loot.amount);
        }

        inventory.onInventoryChanged?.Invoke();

        Destroy(gameObject);
    }
}
