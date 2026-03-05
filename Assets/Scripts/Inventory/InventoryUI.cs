using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public InventorySlotUI[] slotsUI;

    void Start()
    {
        RefreshUI();

        inventory.onInventoryChanged += RefreshUI;
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slotsUI.Length; i++)
        {
            if (i < inventory.slots.Count)
            {
                slotsUI[i].SetSlot(inventory.slots[i]);
            }
        }
    }
}