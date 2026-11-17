using System.Collections;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _blinkInterval;

    private bool _isAlarmActive;

    private void Awake()
    {
        if (_light != null)
            _light.enabled = false;
    }

    public void SetAlarmActive(bool isActive)
    {
        if (_isAlarmActive == isActive)
            return;

        _isAlarmActive = isActive;
        StopAllCoroutines();

        if (_light == null)
            return;

        if (isActive)
        {
            _light.enabled = true;
            StartCoroutine(Blink());
        }
        else
        {
            _light.enabled = false;
        }
    }

    private IEnumerator Blink()
    {
        var wait = new WaitForSeconds(_blinkInterval);

        while (_isAlarmActive && isActiveAndEnabled)
        {
            yield return wait;

            if (_isAlarmActive)
                _light.enabled = !_light.enabled;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _isAlarmActive = false;

        if (_light != null)
            _light.enabled = false;
    }
}