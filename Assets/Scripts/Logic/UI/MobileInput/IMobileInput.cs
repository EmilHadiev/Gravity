using System;

public interface IMobileInput
{
    Joystick Joystick { get; }

    event Action Attacked;
    event Action Jumped;
}