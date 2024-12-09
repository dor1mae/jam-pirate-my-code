public class ConsumableInteractor : ItemInteractor
{
    protected override AbstractItem CreateInstance()
    {
        switch ((_itemForSpawn as СonsumablesConfig).Type)
        {
            case СonsumablesConfig.ConsumablesType.Stamina:
                return new StaminaConsumable(_itemForSpawn);

            case СonsumablesConfig.ConsumablesType.Health:
                return new HealthConsumable(_itemForSpawn);

            default:
                return null;
        }
    }
}
