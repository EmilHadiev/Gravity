using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const int LeftMouseButton = 0;
    private const KeyCode SpaceButton = KeyCode.Space;

    public event Action Attacked;
    public event Action Jumped;    

    public float GetHorizontal() => Input.GetAxisRaw(Horizontal);
    public float GetVertical() => Input.GetAxisRaw(Vertical);

    public void Tick()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
            Attacked?.Invoke();

        if (Input.GetKeyDown(SpaceButton))
            Jumped?.Invoke();
    }
}