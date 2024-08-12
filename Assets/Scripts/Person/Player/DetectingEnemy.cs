using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayerMask;

    private Transform _closestEnemy;
    private float _closestEnemyDistance = Mathf.Infinity;

    public Transform GetClosestEnemy()
    {
        return _closestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_enemyLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            Transform enemyTransform = collision.transform;

            float distanceToEnemy = (enemyTransform.position - transform.position).sqrMagnitude;

            if (_closestEnemy == null || distanceToEnemy < _closestEnemyDistance)
            {
                _closestEnemy = enemyTransform;
                _closestEnemyDistance = distanceToEnemy;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_enemyLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (collision.transform == _closestEnemy)
            {
                _closestEnemy = null;
                _closestEnemyDistance = Mathf.Infinity;
            }
        }
    }
}
