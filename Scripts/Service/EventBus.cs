using System;

public static class EventBus
{
    public static Action<HealthController> Death;
    public static Action<Tile> SendTile;
    public static Func<ItemUIObject, Tile, bool> SetItemInNewPlace;
}
