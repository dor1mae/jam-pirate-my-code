public class StaminaConsumable : UsableItem
{
    public StaminaConsumable(AbstractScriptableItem item) : base(item)
    {
    }

    public override void Use()
    {
        Hero.GetStaminaController.Invoke().AddStamina((_scriptedItem as СonsumablesConfig).RestoreValue);
        _inventory.RemoveItem(this);
    }
}
