public static class AssetProvider
{
    public enum Player
    {
        Player,
        PlayerObby
    };

    #region Swords
    public enum Swords
    {
        SwordIron,
        SwordDiamond,
        SwordGold,
        SwordNether,
        BatSword,
        DarkPumpkinSword,
        HammerSword,
        SwordLinked,
        SwordWood
    }
    #endregion

    #region Particles
    public enum Particles
    {
        ParticleDamageImpact,
        PartcleDamageText
    }
    #endregion

    #region Enemies
    public enum Enemies
    {
        HuggyWuggy
    }
    #endregion

    public const string MobileCanvas = nameof(MobileCanvas);
}