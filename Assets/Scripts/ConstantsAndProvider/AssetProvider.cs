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
        SwordWood,
        SwordIron,
        SwordGold,
        SwordDiamond,
        SwordNether,

        BatSword,
        DarkPumpkinSword,
        HammerSword,
        LinkedSword        
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