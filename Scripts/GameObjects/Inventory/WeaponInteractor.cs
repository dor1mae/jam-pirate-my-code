public class WeaponInteractor : ItemInteractor
{
    protected override AbstractItem CreateInstance()
    {
        return new Weapon(_itemForSpawn);
    }
}
