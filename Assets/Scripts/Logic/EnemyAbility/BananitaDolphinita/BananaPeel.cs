using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class BananaPeel : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private float _pushAwayDistance = 5f;
    [SerializeField] private float _pusAwayDuration = 0.1f;

    [Inject] private readonly IEnemySoundContainer _sound;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
    }

    private void OnPlayerEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IMovable movable))
        {
            _sound.Play(AssetProvider.Sounds.Slip.ToString());
            var transform = movable.Transform;

            if (DOTween.IsTweening(transform))
                return;

            transform.DOMove(transform.forward * _pushAwayDistance, _pusAwayDuration);
        }
    }
}