using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _blinkInterval;

    private bool _isAlarmActive;
    private float _blinkTimer;

    private void Awake()
    {
        if (_light != null)
            _light.enabled = false;
    }

    private void Update()
    {
        if (_isAlarmActive == false || _light == null)
            return;

        _blinkTimer += Time.deltaTime;

        if (_blinkTimer < _blinkInterval)
            return;

        _blinkTimer = 0f;
        _light.enabled = !_light.enabled;
    }

    public void SetAlarmActive(bool isActive)
    {
        _isAlarmActive = isActive;
        _blinkTimer = 0f;

        if (_light != null)
            _light.enabled = isActive;
    }

    private void OnDisable()
    {
        _isAlarmActive = false;
        _blinkTimer = 0f;

        if (_light != null)
            _light.enabled = false;
    }
}