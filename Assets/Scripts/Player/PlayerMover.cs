using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speedDirection;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private Coin _сoin;

    private readonly int Speed = Animator.StringToHash(nameof(Speed));
    private Rigidbody2D _rigidbody;
    private float _direction;
    private bool _isJump = false;
    private bool _isGrounded = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = Input.GetAxis(Horizontal);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (_isJump)
        {
            Jump();
            _isJump = false;
        }
    }

    private void Move()
    {
        Vector3 scale = transform.localScale;

        _rigidbody.velocity = new Vector2(_direction * _speedDirection, _rigidbody.velocity.y);

        if (_direction > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else if (_direction < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        _animator.SetFloat(PlayerAnimatorData.Params.Speed, Mathf.Abs(_direction));
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = false;
        }
    }
}