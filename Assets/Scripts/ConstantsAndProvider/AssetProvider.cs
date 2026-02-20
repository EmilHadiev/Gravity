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

        BraidSword,
        DarkPumpkinSword,
        HammerSword,
        LinkedSword        
    }
    #endregion

    #region Particles
    public enum Particles
    {
        ParticleDamageImpact,
        PartcleDamageText,
        GroundHit
    }
    #endregion

    #region Enemies
    public enum Enemies
    {
        HuggyWuggy,
        TungSahur
    }
    #endregion

    #region Sound
    public enum Sounds
    {
        Death,
        Attack,
        PlayerTakeDamage,
        Jump,
        AttackMiss
    }
    #endregion

    #region Scenes
    public enum Scenes
    {
        Arena
    }

    #endregion

    public const string MobileCanvas = nameof(MobileCanvas);
}