[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;

    public bool IsEmpty()
    {
        return item == null;
    }
}