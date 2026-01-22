using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCameraFollower();
    }

    private void BindCameraFollower()
    {
        
    }
}