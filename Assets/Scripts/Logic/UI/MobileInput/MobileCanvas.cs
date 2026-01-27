using UnityEngine;

public class MobileCanvas : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    public Joystick Joystick => _joystick;
}