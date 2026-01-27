using UnityEngine;

public class DesktopMovePattern : PlayerMovePattern
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public DesktopMovePattern(IMovable movable, IPlayerAnimator animator) : base(movable, animator)
    {
    }

    protected override float GetHorizontal() => Input.GetAxisRaw(Horizontal);
    protected override float GetVertical() => Input.GetAxisRaw(Vertical);
}