using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IDamage
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _patrolNodes;
    [SerializeField] private float _chaseDistance = 7.5f;

    private NavMeshAgent _agent;
    private Transform _actualTarget;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _actualTarget = _patrolNodes[Random.Range(0, _patrolNodes.Length)];
        _agent.SetDestination(_actualTarget.position);
    }

    private void FixedUpdate()
    {
        if((_target.position - transform.position).sqrMagnitude <= _chaseDistance * _chaseDistance)
        {
            _agent.SetDestination(_target.position);
        }
        else
        {
            if(_agent.destination != _actualTarget.position)
            {
                _agent.SetDestination(_actualTarget.position);
            }

            if((transform.position - _actualTarget.position).sqrMagnitude < 0.25f * 0.25f)
            {
                _actualTarget = _patrolNodes[Random.Range(0, _patrolNodes.Length)];
                _agent.SetDestination(_actualTarget.position);
            }
        }
    }

    public void TakeDamage()
    {
        Debug.Log($"<color=yellow>DIO</color>: WRYYYY!");
    }
}
