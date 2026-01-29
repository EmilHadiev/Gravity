using Cysharp.Threading.Tasks;

public class MobileMovePattern : PlayerMovePattern
{
    private Joystick _joystick;

    public MobileMovePattern(IMovable movable, IPlayerAnimator animator, IFactory factory) : base(movable, animator)
    {
        CreateMobileInput(factory).Forget();
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

    private async UniTaskVoid CreateMobileInput(IFactory factory)
    {
        var prefab = await factory.CreateAsync(AssetProvider.MobileCanvas);
        _joystick = prefab.GetComponent<MobileCanvas>().Joystick;
    }
}