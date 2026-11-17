using System.Collections;
using UnityEngine;

public class AlarmSound : MonoBehaviour
{
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _fadeSpeed;

    private AudioSource _audioSource;
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
        StopAllCoroutines();

        if (isActive)
        {
            if (_audioSource.clip == null)
                return;

            if (_audioSource.isPlaying == false)
                _audioSource.Play();

            StartCoroutine(FadeVolume(_maxVolume, false));
        }
        else if (_audioSource.isPlaying)
        {
            StartCoroutine(FadeVolume(0f, true));
        }
        else
        {
            _audioSource.volume = 0f;
        }
    }

    private IEnumerator FadeVolume(float targetVolume, bool stopAfterFade)
    {
        while (_audioSource.volume < targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        while (_audioSource.volume > targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (stopAfterFade)
            _audioSource.Stop();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _isAlarmActive = false;

        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.volume = 0f;
        }
    }
}