using System;

public class GameOverService : IGameOverService
{
    public event Action Lost;
    public event Action Won;

    public void PlayerLost()
    {
        Lost?.Invoke();
    }

    public void PlayerWon()
    {
        Won?.Invoke();
    }
}