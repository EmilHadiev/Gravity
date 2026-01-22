using UnityEngine;

public interface IMovable
{
    public Transform Transform { get; }
    public float MoveSpeed { get; }
    public float RotateSpeed { get; }
}