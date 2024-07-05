using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const float Epsilon = 0.1f;

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
        _currentWaypoint = ++_currentWaypoint % _waypoints.Length;
    }

    transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
}

    private void FlipSprite()
    {
        if (_waypoints[_currentWaypoint].position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (_waypoints[_currentWaypoint].position.x < transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x); 
        }
    }
}
