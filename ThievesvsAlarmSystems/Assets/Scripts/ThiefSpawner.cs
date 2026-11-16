using System.Collections;
using UnityEngine;

public class ThiefSpawner : MonoBehaviour
{
    [SerializeField] private Thief _prefab;
    [SerializeField] private Transform _housePoint;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(SpawnRepeatedly());
    }

    private IEnumerator SpawnRepeatedly()
    {
        var wait = new WaitForSeconds(_delay);

        while (isActiveAndEnabled)
        {
            SpawnThief();
            yield return wait;
        }
    }

    private void SpawnThief()
    {
        Thief thief = Instantiate(_prefab, transform.position, transform.rotation);
        thief.Initialize(transform, _housePoint);
    }
}
