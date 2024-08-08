using UnityEngine;

public class ChaseBehavior : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _stopDistance = 1f;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _playerLayer;

    private Transform _playerTransform;
    private Enemy _enemy;

    private bool _isChasingPlayer = false;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        CheckForPlayer();

        if (_isChasingPlayer)
        {
            MoveTowardsPlayer();
        }

        FlipSprite();
    }

    private void CheckForPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, _detectionRange, _playerLayer);

        if (playerCollider != null)
        {
            _isChasingPlayer = true;
            _playerTransform = playerCollider.transform;
        }
        else
        {
            _isChasingPlayer = false;
            _playerTransform = null;
        }
    }

    private void MoveTowardsPlayer()
    {
        float sqrDistanceToPlayer = (transform.position - _playerTransform.position).sqrMagnitude;

        if (sqrDistanceToPlayer > _stopDistance * _stopDistance)
        {
            Vector3 targetPosition = _playerTransform.position;
            targetPosition.y = transform.position.y;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }

    private void FlipSprite()
    {
        Vector3 scale = _enemy.SpritePerson.transform.localScale;

        if (_playerTransform != null)
        {
            scale.x = _playerTransform.position.x > transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        }

        _enemy.SpritePerson.transform.localScale = scale;
    }
}
