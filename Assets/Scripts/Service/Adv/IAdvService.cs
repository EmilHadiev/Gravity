using System;

public interface IAdvService
{
    void ShowInterstitial();
    void ShowRewardAdv(Action giveReward);
}