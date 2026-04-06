using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private AssetProvider.Scenes _sceneToLoad;

    [Inject] private readonly ISavable _saver;
    [Inject] private readonly ISceneLoader _sceneLoader;

    private void Start()
    {
        LoadData();
        SwitchScene();
    }

    private void LoadData()
    {
        _saver.Load();
    }

    private void SwitchScene()
    {
        _sceneLoader.SwitchTo(_sceneToLoad.ToString());
    }
}