public interface IPlayer
{
    PlayerData Data { get; }
    IPlayerAnimator Animator { get; }
    IMovable Mover { get; }
}