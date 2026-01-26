using DG.Tweening;
using Zenject;

public class TweenOptimizator : IInitializable
{
    public void Initialize()
    {
        DOTween.SetTweensCapacity(500, 50);
    }
}