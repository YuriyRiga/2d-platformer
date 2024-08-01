using UnityEngine;
using System;

public class PatrolBehavior : MonoBehaviour
{
    private const float _epsilon = 0.1f;

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private int _currentWaypoint;

    private void Update()
    {
        MoveTowardsNextWaypoint();
        FlipSprite();

    }

    private void MoveTowardsNextWaypoint()
    {
        float sqrDistanceToWaypoint = (transform.position - _waypoints[_currentWaypoint].position).sqrMagnitude;

        if (sqrDistanceToWaypoint < _epsilon)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }

    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;

        scale.x = _waypoints[_currentWaypoint].position.x > transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        
        transform.localScale = scale;
    }
}