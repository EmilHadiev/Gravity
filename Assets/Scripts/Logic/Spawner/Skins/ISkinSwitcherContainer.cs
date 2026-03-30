using System;

public interface ISkinSwitcherContainer
{
    event Action<ItemData> PlayerEntered;
    event Action PlayerExited;

    void TrySwitchSkin();
}