using System;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _arrivalDistance;

    private Transform _spawnPoint;
    private Transform _target;
    private bool _isReturning;

    public void Initialize(Transform spawnPoint, Transform housePoint)
    {
        _spawnPoint = spawnPoint;
        _target = housePoint;
        _isReturning = false;
    }

    private void Update()
    {
        Vector3 targetPosition = _target.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        transform.LookAt(targetPosition);

        Vector3 offset = targetPosition - transform.position;

        if(offset.sqrMagnitude <= _arrivalDistance * _arrivalDistance)
            HandleTargetReached();
    }

    private void HandleTargetReached()
    {
        if (_isReturning)
        {
            Destroy(gameObject);
            return;
        }

        _isReturning = true;
        _target = _spawnPoint;
    }
}
