using UnityEngine;

[RequireComponent(typeof(PatrolBehavior))]
[RequireComponent(typeof(ChaseBehavior))]

public class CheckPlayer : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _offsetCircle = 0f;
    [SerializeField] private LayerMask _playerLayer;

    private PatrolBehavior _patrolBehavior;
    private ChaseBehavior _chaseBehavior;

    private void Awake()
    {
        _patrolBehavior = GetComponent<PatrolBehavior>();
        _chaseBehavior = GetComponent<ChaseBehavior>();
    }

    private void Update()
    {
        DetectAndSwitchBehaviors(); 
    }

    private void DetectAndSwitchBehaviors()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + _offsetCircle), _detectionRange, _playerLayer);

        if (playerCollider != null)
        {
            _patrolBehavior.enabled = false;
            _chaseBehavior.enabled = true;
        }
        else
        {
            _patrolBehavior.enabled = true;
            _chaseBehavior.enabled = false;
        }
    }
}


