using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Tiny.Trail))]
public class SwordTrial : MonoBehaviour
{
    [SerializeField] private Tiny.Trail _trail;

    private const int HideDelay = 600;

    private CancellationTokenSource _cts;

    private void OnValidate()
    {
        _trail ??= GetComponent<Tiny.Trail>();
    }

    public void SetColor(Color color)
    {
        _trail.material.color = color;
    }

    public void TrialToggle(bool isOn)
    {
        _trail.enabled = isOn;

        if (isOn)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            HideTrialImmediatly().Forget();
        }
    }

    private async UniTask HideTrialImmediatly()
    {
        await UniTask.Delay(HideDelay, cancellationToken: _cts.Token);
        _trail.enabled = false;
    }


    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
}