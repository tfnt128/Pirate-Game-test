using UnityEngine;

public class GenericEnemyFollow : MonoBehaviour
{
    public float enemySpeed = 1f;
    public bool canPursue = true;

    private Transform _player;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_player != null)
        {
            Vector3 targetDirection = _player.position - transform.position;
            targetDirection.Normalize();

            RotateEnemy(targetDirection);
            MoveEnemy(targetDirection);
        }
    }

    void MoveEnemy(Vector3 direction)
    {
        if(canPursue)
            transform.Translate(direction * enemySpeed * Time.deltaTime, Space.World);
    }

    void RotateEnemy(Vector3 lookDirection)
    {
        transform.LookAt(transform.position + new Vector3(0, 0, 1), lookDirection);
    }
}
