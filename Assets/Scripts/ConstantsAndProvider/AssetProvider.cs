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
        Slip
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