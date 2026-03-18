public interface IShopWindowStateMachine
{
    public void Switch<T>() where T : IShopWindowState;
}