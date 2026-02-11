using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class SceneSwitcherButton : MonoBehaviour
{
    [SerializeField] private AssetProvider.Scenes _sceneName;
    [SerializeField] private Button _button;

    [Inject] private readonly ISceneLoader _sceneLoader;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SwitchScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SwitchScene);
    }

    private void SwitchScene()
    {
        _sceneLoader.Restart();
    }
}