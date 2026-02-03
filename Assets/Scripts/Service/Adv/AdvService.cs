using System;
using YG;

public class AdvService : IAdvService
{
    public void ShowInterstitial()
    {
        YG2.InterstitialAdvShow();
    }

    public void ShowRewardAdv(Action giveReward)
    {
        YG2.RewardedAdvShow("", () =>
        {
            giveReward?.Invoke();
        });
    }
}