public class Key : AbstractItem
{
    public Key(AbstractScriptableItem item) : base(item)
    {
    }

    public override void Use()
    {
        _inventory.RemoveItem(this);
    }
}

