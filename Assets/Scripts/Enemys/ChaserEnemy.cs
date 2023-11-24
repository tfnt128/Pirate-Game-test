using System;
using Unity.Mathematics;
using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    public float enemySpeed = 1f;

    [SerializeField] private GameObject explosionEffect;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(explosionEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }

    void MoveEnemy(Vector3 direction)
    {
        transform.Translate(direction * enemySpeed * Time.deltaTime, Space.World);
    }

    void RotateEnemy(Vector3 lookDirection)
    {
        transform.LookAt(transform.position + new Vector3(0, 0, 1), lookDirection);
    }
}
