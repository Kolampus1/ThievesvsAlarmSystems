using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private HouseZone _houseZone;
    [SerializeField] private AlarmLight _alarmLight;
    [SerializeField, Range(0f, 1f)] private float _maxVolume = 1f;
    [SerializeField] private float _volumeChangeSpeed = 1f;

    private AudioSource _audioSource;
    private int _thiefCount;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (_houseZone == null)
            return;

        _houseZone.ThiefCountChanged += HandleThiefCountChanged;
        HandleThiefCountChanged(_houseZone.ThiefCount);
    }

    private void Update()
    {
        float targetVolume = _thiefCount > 0 ? _maxVolume : 0f;

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);

        if (_thiefCount == 0 && _audioSource.isPlaying && _audioSource.volume <= 0f)
            _audioSource.Stop();
    }

    private void HandleThiefCountChanged(int thiefCount)
    {
        _thiefCount = thiefCount;

        bool isAlarmActive = _thiefCount > 0;

        if (_alarmLight != null)
            _alarmLight.SetAlarmActive(isAlarmActive);

        if (isAlarmActive && _audioSource.clip != null && _audioSource.isPlaying == false)
            _audioSource.Play();
    }

    private void OnDisable()
    {
        if (_houseZone != null)
            _houseZone.ThiefCountChanged -= HandleThiefCountChanged;

        _thiefCount = 0;

        if (_alarmLight != null)
            _alarmLight.SetAlarmActive(false);

        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.volume = 0f;
        }
    }
}