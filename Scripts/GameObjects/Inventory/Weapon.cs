public class Weapon : UsableItem
{
    public override void Use()
    {
        //Логика смены оружия через инвентарь
        WeaponHolderReplacer.SendWeapon?.Invoke(this);
    }

    public Weapon(AbstractScriptableItem item) : base(item)
    {
    }

}
