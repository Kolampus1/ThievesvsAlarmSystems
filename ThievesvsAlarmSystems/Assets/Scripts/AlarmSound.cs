using System.Collections;
using UnityEngine;

public class AlarmSound : MonoBehaviour
{
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _fadeSpeed;

    private AudioSource _audioSource;
    private Coroutine _fadeCoroutine;
    private bool _isAlarmActive;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAlarmActive(bool isActive)
    {
        if (_isAlarmActive == isActive)
            return;

        _isAlarmActive = isActive;

        if (_isAlarmActive && _audioSource.clip != null && _audioSource.isPlaying == false)
            _audioSource.Play();

        if (_fadeCoroutine == null)
            _fadeCoroutine = StartCoroutine(FadeVolume());
    }

    private IEnumerator FadeVolume()
    {
        float targetVolume = _isAlarmActive ? _maxVolume : 0f;

        while (_audioSource.volume != targetVolume)
        {
            targetVolume = _isAlarmActive ? _maxVolume : 0f;

            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        if (_isAlarmActive == false)
            _audioSource.Stop();

        _fadeCoroutine = null;
    }
}