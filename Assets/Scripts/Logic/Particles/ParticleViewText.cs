using CartoonFX;
using UnityEngine;

[RequireComponent(typeof(CFXR_ParticleText))]
public class ParticleViewText : ParticleView
{
    [SerializeField] private CFXR_ParticleText _text;

    private void OnValidate()
    {
        _text ??= GetComponent<CFXR_ParticleText>();
    }

    public void ChangeText(string text)
    {
        _text.UpdateText(text);
    }
}
