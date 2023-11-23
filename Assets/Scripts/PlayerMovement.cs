using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private float _rotationAngle = 0;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerMaxSpeed;
    [SerializeField] private float playerRotationSpeed;
    [SerializeField] [Range(0, 1)] private float playerDriftRange = 1;
    [SerializeField] [Range(0, 3)] private float playerDragRange = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _direction.y = Input.GetAxisRaw("Vertical");
        _direction.x = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        ApplySpeed();
    }

    private void ApplySpeed()
    {
        LimitVelocity();
        ApplyRotation();
        ApplyDrift();
        DragCheck();
    }

    private void LimitVelocity()
    {
        if (_direction.y < 0) return;

        var velocityUp = Vector2.Dot(transform.up, _rb.velocity);
        if (velocityUp >= playerMaxSpeed) return;

        _rb.AddForce(transform.up * -_direction.y * playerSpeed, ForceMode2D.Force);
    }

    private void ApplyRotation()
    {
        _rotationAngle -= _direction.x * playerRotationSpeed;
        _rb.MoveRotation(_rotationAngle);
    }

    private void ApplyDrift()
    {
        Vector2 velocityUp = transform.up * Vector2.Dot(_rb.velocity, transform.up);
        Vector2 velocityRight = transform.right * Vector2.Dot(_rb.velocity, transform.right);
        _rb.velocity = velocityUp + velocityRight * playerDriftRange;
    }

    private void DragCheck()
    {
        _rb.drag = (_direction.y <= 0) ? Mathf.Lerp(_rb.drag, playerDragRange, Time.deltaTime) : 0;
    }
}