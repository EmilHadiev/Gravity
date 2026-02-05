using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SwordSwitchContainer : MonoBehaviour
{
    [SerializeField] private SwordSwitcher _template;
    [SerializeField] private float _additionalX = 6f;

    [Inject] private readonly IFactory _factory;
    [Inject] private readonly SwordData[] _swordData;

    private readonly List<SwordSwitcher> _switchs = new List<SwordSwitcher>();

    private void Start()
    {
        CreateTemplates().Forget();
    }

    private async UniTaskVoid CreateTemplates()
    {
        for (int i = 0; i < _swordData.Length; i++)
        {
            await UniTask.NextFrame();
            var prefab = _factory.Create(_template.gameObject);            
            SetPosition(prefab.transform, i);
            var swordView = prefab.GetComponent<SwordSwitcher>();
            swordView.ShowSwordInfo(_swordData[i]);
            _switchs.Add(swordView);
        }
    }

    private void SetPosition(Transform swordPrefab, int mult)
    {
        swordPrefab.parent = transform;
        var pos = transform.position;
        Vector3 newPos = new Vector3(_additionalX * mult, pos.y, pos.z);
        swordPrefab.transform.localPosition = newPos;
    }
}