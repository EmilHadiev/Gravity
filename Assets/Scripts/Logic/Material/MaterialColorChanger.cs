using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private MaterialPropertyBlock _propertyBlock;

    public readonly Color Gold = new(1, 0.8392157f, 0, 1);

    private void Awake()
    {
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void SetColor(string colorID = "_MainColor", Color color = default)
    {
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetColor(colorID, color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}