using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const float _epsilon = 0.1f;

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance = 1f;

    private int _currentWaypoint;
    private bool _isChasingPlayer = false;
    private Transform _playerTransform;

    private void Update()
    {
        if (_isChasingPlayer == false)
        {
            MoveTowardsNextWaypoint();
        }
        else
        {
            MoveTowardsPlayer();
        }

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

    private void MoveTowardsPlayer()
    {
        float sqrDistanceToPlayer = (transform.position - _playerTransform.position).sqrMagnitude;

        if (sqrDistanceToPlayer > _stopDistance)
        {
            Vector3 targetPosition = _playerTransform.position;
            targetPosition.y = transform.position.y; 

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }

    private void FlipSprite()
    {
        Vector3 scale = transform.localScale;

        if (_isChasingPlayer && _playerTransform != null)
        {
            if (_playerTransform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else if (_playerTransform.position.x < transform.position.x)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
        }
        else
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

        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out _))
        {
            _isChasingPlayer = true;
            _playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out  _))
        {
            _isChasingPlayer = false;
            _playerTransform = null;
        }
    }
}
