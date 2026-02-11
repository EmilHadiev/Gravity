using UnityEngine;

[RequireComponent(typeof(Tiny.Trail))]
public class SwordTrial : MonoBehaviour
{
    [SerializeField] private Tiny.Trail _trail;

    private void OnValidate()
    {
        _trail ??= GetComponent<Tiny.Trail>();
    }

    public void SetColor(Color color)
    {
        _trail.material.color = color;
    }
}