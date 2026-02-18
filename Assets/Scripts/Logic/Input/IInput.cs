using System;

public interface IInput
{
    event Action Attacked;
    event Action Jumped;

    float GetVertical();
    float GetHorizontal();
}