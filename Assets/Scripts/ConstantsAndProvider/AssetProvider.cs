public static class AssetProvider
{
    public const string PlayerViewPrefix = "_View";

    #region PlayerSkins
    public enum Player
    {
        PlayerObby,
        Herobrine,
        Jojo,
        Guard
    };
    #endregion

    #region Items
    public enum Cloth
    {
        Crown,
        Cape,
        Glasses
    }
    #endregion

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
        GroundHit,
        Explosion,
        FireBall
    }
    #endregion

    #region Enemies
    public enum Enemies
    {
        //Common
        Fluriflura,
        LiriliLaria,
        NoobiniPizzanini,
        SvininaBombardino,
        TimCheese,

        //Rare
        BanditoBobritto,
        BonecaAmbalabu,
        PipiAvocado,
        TricTracBaraboom,
        TungTungTungSahur,

        //Epic
        BananitaDolphinita,
        BrrBrrPatapim,
        BrriBrriBicusDicusBombicus,
        CappuccinoAssassino,
        PerochelloLemonchello
    }
    #endregion

    #region Sound
    public enum Sounds
    {
        Death,
        Attack,
        PlayerTakeDamage,
        Jump,
        AttackMiss,
        Explosion,
        Shooting,
        Slip,
        Click,
        AddCoins
    }
    #endregion

    #region Scenes
    public enum Scenes
    {
        Arena
    }

    #endregion

    #region CrystallsCount
    public enum CrystallsCount
    {
        Crystall_10 = 10,
        Crystall_50 = 50,
        Crystall_100 = 100
    }
    #endregion

    public const string MobileCanvas = nameof(MobileCanvas);
}