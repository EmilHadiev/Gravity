public class PlayerFinder : IPlayerFinder
{
    public IPlayer Player { get; private set; }

    public void SetPlayer(IPlayer player)
    {
        Player = player;
    }
}