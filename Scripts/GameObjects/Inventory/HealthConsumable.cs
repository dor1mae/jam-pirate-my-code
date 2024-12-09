public class HealthConsumable : UsableItem
{
    public HealthConsumable(AbstractScriptableItem item) : base(item)
    {
    }

    public override void Use()
    {
        Hero.GetHealthController.Invoke().AddHealth((_scriptedItem as СonsumablesConfig).RestoreValue);
        _inventory.RemoveItem(this);
    }
}
