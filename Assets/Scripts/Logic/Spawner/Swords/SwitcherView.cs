using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SwitcherView : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;

    [Inject] private readonly IFactory _factory;

    public void CreateSwordView(AssetProvider.Swords newSword)
    {
        CreateSword(newSword).Forget();
    }

    private async UniTaskVoid CreateSword(AssetProvider.Swords newSword)
    {
        var prefab = await _factory.CreateAsync(newSword.ToString());
        SetPosition(prefab.transform);
        DisableComponents(prefab);
    }

    private void SetPosition(Transform view)
    {
        view.transform.parent = transform;
        var rotation = Quaternion.Euler(_rotation);
        view.transform.SetPositionAndRotation(transform.position, rotation);
    }

    private void DisableComponents(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (component is Transform) continue; // Transform нельзя отключить

            // Если у компонента есть свойство enabled
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = false;
            }
            // Для Collider/Renderer и других, работающих через enabled
            if (component is Collider collider) collider.enabled = false;
            if (component is Renderer renderer) renderer.enabled = false;
        }
    }
}