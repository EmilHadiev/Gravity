public class MobileMovePattern : PlayerMovePattern
{
    private Joystick _joystick;

    public MobileMovePattern(IMovable movable, IPlayerAnimator animator, IMobileInput mobileInput) : base(movable, animator)
    {
        _joystick = mobileInput.Joystick;
    }

    protected override float GetHorizontal()
    {
        if (_joystick != null)
            return _joystick.Horizontal;

        return 0;
    }

    protected override float GetVertical()
    {
        if (_joystick != null)
            return _joystick.Vertical;

        return 0;
    }
}