using System;

public interface IPlayerSpawner
{
    event Action<IPlayer> PlayerSpawned;

    void Spawn(ICameraFollower cameraFollower);
}