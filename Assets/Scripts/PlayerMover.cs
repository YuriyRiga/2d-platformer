using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = (nameof(Horizontal));

    [SerializeField] private float _speedDirection;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Coin _ñoin;

    private Rigidbody2D _rigidbody;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float direction = Input.GetAxis(Horizontal);
        _rigidbody.velocity = new Vector2(direction * _speedDirection, _rigidbody.velocity.y);

        if (direction > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        _animator.SetFloat("Speed", Mathf.Abs(direction));
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }

        if (collision.collider.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
