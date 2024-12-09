public class KeyInteractor : ItemInteractor
{
    protected override AbstractItem CreateInstance()
    {
        return new Key(_itemForSpawn);
    }
}
