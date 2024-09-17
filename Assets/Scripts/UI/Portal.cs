using System.Collections;

using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Material _centerMaterial;
    private PlayerActions _player;
    private bool _isReadyToExit;
    private float _emissionIntensity = 1f;

    private void Start()
    {
        QuestHandler.instance.OnKeysReceived += QuestCompleteEvent;
        _centerMaterial.SetColor("_EmissionColor", _centerMaterial.color * _emissionIntensity * 35);
    }
    private void QuestCompleteEvent()
    {
        _isReadyToExit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _player = other.GetComponent<PlayerActions>();
        if (_isReadyToExit && _player != null)
        {
            QuestHandler.instance.OnEndGame(true);
            StartCoroutine(SetCenterBrightness());
        }
    }

    private IEnumerator SetCenterBrightness()
    {
        while (_emissionIntensity < 150)
        {
            yield return new WaitForSeconds(0.01f);
            _centerMaterial.SetColor("_EmissionColor", _centerMaterial.color * _emissionIntensity * 100);
            yield return null;
        }
    }
}