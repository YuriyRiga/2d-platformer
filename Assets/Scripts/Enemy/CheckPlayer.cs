using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _offsetCircle = 0f;
    [SerializeField] private LayerMask _playerLayer;
    private Vector2 playerTransform;

    private void Update()
    {
        CheckForPlayer(); 
    }

    private void CheckForPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + _offsetCircle), _detectionRange, _playerLayer);

        if (playerCollider != null)
        {
            gameObject.GetComponent<PatrolBehavior>().enabled = false;
            gameObject.GetComponent<ChaseBehavior>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<PatrolBehavior>().enabled = true;
            gameObject.GetComponent<ChaseBehavior>().enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + _offsetCircle), _detectionRange);
    }
}


