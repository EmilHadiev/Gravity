using System;

public interface ISwordSwitchContainer
{
    event Action<ItemData> PlayerEntered;
    event Action PlayerExited;

    public void TrySwitchSword();
}