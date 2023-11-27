using UnityEngine;

public class GenericEnemyFollow : MonoBehaviour
{
    public bool CanPursue { get; set; } = true;
    
    [Header("Components")]
    [SerializeField] private float speed = 1f;
    
    private Transform _playerTransform;

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (_playerTransform != null)
        {
            Vector3 targetDirection = GetTargetDirection();
            RotateEnemy(targetDirection);
            MoveEnemy(targetDirection);
        }
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    private Vector3 GetTargetDirection()
    {
        if (_playerTransform != null)
        {
            Vector3 direction = _playerTransform.position - transform.position;
            direction.Normalize();
            return direction;
        }
        return Vector3.zero;
    }

    private void MoveEnemy(Vector3 direction)
    {
        if (CanPursue) transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void RotateEnemy(Vector3 lookDirection)
    {
        transform.LookAt(transform.position + new Vector3(0, 0, 1), lookDirection);
    }
}