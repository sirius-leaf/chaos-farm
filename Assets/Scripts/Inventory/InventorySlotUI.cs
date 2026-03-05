using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountText;
    public int slotId = 0;

    private Inventory inventory;
    private Image slotFrame;

    public void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        slotFrame = GetComponent<Image>();
    }

    public void Update()
    {
        if (inventory.selectedSlot != slotId)
            slotFrame.color = new(0f, 0f, 0f, 0.5f);
        else
            slotFrame.color = new(0f, 0.5f, 0f, 0.5f);
    }

    public void SetSlot(InventorySlot slot)
    {
        if (slot.item == null)
        {
            icon.enabled = false;
            amountText.text = "";
            return;
        }

        icon.enabled = true;
        icon.sprite = slot.item.icon;

        amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";
    }
}