using UnityEngine;
using YG;

[CreateAssetMenu(menuName = "Data/Env", fileName = "EnvData")]
public class EnvData : ScriptableObject
{
    [SerializeField] public bool IsDesktop => YG2.envir.isDesktop;
}