using System;

public interface ISkinViewSwitcherContainer
{
    event Action<ItemData> PlayerEntered;
    event Action PlayerExited;
}
