using System;

public interface IPlayerSpawner
{
    event Action<IPlayer> PlayerSpawned;

    public IPlayer Player { get; }

    void Spawn(ICameraFollower cameraFollower);
}