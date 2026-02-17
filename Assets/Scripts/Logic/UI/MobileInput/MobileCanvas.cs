using System;
using UnityEngine;
using UnityEngine.UI;

public class MobileCanvas : MonoBehaviour, IMobileInput
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _attack;
    [SerializeField] private Button _jump;

    public Joystick Joystick => _joystick;
    public event Action Jumped;
    public event Action Attacked;

    private void OnEnable()
    {
        _attack?.onClick.AddListener(Attack);
        _jump?.onClick.AddListener(Jump);               
    }

    private void OnDisable()
    {
        _attack?.onClick.RemoveListener(Attack);
        _jump?.onClick.RemoveListener(Jump);
    }

    private void Jump() => Jumped?.Invoke();
    private void Attack() => Attacked?.Invoke();
}