using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public System.Action onInventoryChanged;
    public int slotCount = 10;
    public int selectedSlot = -1;

    void Awake()
    {
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        // Jika stackable, coba tambahkan ke slot yang sudah ada
        if (item.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == item && slot.amount < item.maxStack)
                {
                    slot.amount += amount;
                    return true;
                }
            }
        }

        // Cari slot kosong
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.item = item;
                slot.amount = amount;
                return true;
            }
        }

        return false; // Inventory penuh
    }

    public void SelectSlot(int index)
    {
        selectedSlot = index;
    }
    
    public InventorySlot GetSelectedSlot()
    {
        if (selectedSlot < 0 || selectedSlot >= slots.Count)
            return null;

        return slots[selectedSlot];
    }
}