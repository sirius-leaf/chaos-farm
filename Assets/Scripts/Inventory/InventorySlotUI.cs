using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountText;

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