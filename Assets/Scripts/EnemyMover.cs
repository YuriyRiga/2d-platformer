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
        if (Vector2.Distance(transform.position, _waypoints[_currentWaypoint].position) < _epsilon)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (_waypoints[_currentWaypoint].position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_waypoints[_currentWaypoint].position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
