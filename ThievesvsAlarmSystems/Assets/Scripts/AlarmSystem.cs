using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private HouseZone _houseZone;
    [SerializeField] private AlarmSound _alarmSound;
    [SerializeField] private AlarmLight _alarmLight;

    private void OnEnable()
    {
        if (_houseZone == null)
            return;

        _houseZone.AlarmStateChanged += HandleAlarmStateChanged;
        HandleAlarmStateChanged(_houseZone.IsAlarmActive);
    }

    private void OnDisable()
    {
        if (_houseZone != null)
            _houseZone.AlarmStateChanged -= HandleAlarmStateChanged;

        HandleAlarmStateChanged(false);
    }

    private void HandleAlarmStateChanged(bool isActive)
    {
        if (_alarmSound != null)
            _alarmSound.SetAlarmActive(isActive);

        if (_alarmLight != null)
            _alarmLight.SetAlarmActive(isActive);
    }
}