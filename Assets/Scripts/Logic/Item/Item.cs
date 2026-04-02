using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private AssetProvider.Item _item;

    public AssetProvider.Item ITem => _item;

    /// <summary>
    /// only local
    /// </summary>
    /// <param name="scale"></param>
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetPositionAndRotation(Vector3 position, Vector3 rotation, bool isLocal = true)
    {
        var eulerRotation = Quaternion.Euler(rotation);

        if (isLocal)
            transform.SetLocalPositionAndRotation(position, eulerRotation);
        else
            transform.SetPositionAndRotation(position, eulerRotation);
    }
}