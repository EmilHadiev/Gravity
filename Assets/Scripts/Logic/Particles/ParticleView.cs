using UnityEngine;

public class ParticleView : MonoBehaviour
{
    public void Play()
    {
        Stop();
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}