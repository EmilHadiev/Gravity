public interface IPlayerFinder
{
    IPlayer Player { get; }

    void SetPlayer(IPlayer player);
}