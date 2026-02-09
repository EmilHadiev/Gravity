using Zenject;

public class PlayerDieLogic : DieLogic
{
    private IGameOverService _gameOverService;
    private IPlayerSoundContainer _playerSound;

    [Inject]
    private void Contructor(IGameOverService gameOverService, IPlayerSoundContainer playerSound)
    {
        _gameOverService = gameOverService;
        _playerSound = playerSound;
    }

    protected override void OnDie()
    {
        _gameOverService.PlayerLost();
        _playerSound.Play(AssetProvider.Sounds.Death.ToString());
        gameObject.SetActive(false);
    }
}