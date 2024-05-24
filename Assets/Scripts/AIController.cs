using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public enum IAState
{
    None,
    Idle,
    Patrol,
    PlayerSeen,
    PlayerNear
}
public class AIController : MonoBehaviour
{

    private IAState _state = IAState.None;
    [SerializeField] private Animator _animator;
    public bool PlayerNear = false;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _waypoint;
    private void Update()
    {
        CheckTransition();
        Behavior();
    }

    private void Behavior()
    {
        switch (_state)
        {
            case IAState.None:
                break;
            case IAState.Idle:
                break;
            case IAState.Patrol:
                //find next destination
                _agent.SetDestination(_waypoint.transform.position);
                break;
            case IAState.PlayerSeen:
                break;
            case IAState.PlayerNear:
                break;
        }
    }

    private void CheckTransition()
    {
        switch (_state)
        {
            case IAState.None:
                break;
            case IAState.Idle:
                if (PlayerNear)
                {
                    _state = IAState.PlayerNear;
                    _animator.SetBool("IsPlayerNear", true);
                }
                break;
            case IAState.Patrol:
                if (PlayerNear)
                {
                    _state = IAState.PlayerNear;
                    _animator.SetBool("IsPlayerNear", true);
                }
                break;
            case IAState.PlayerSeen:
                if (PlayerNear)
                {
                    _state = IAState.PlayerNear;
                    _animator.SetBool("IsPlayerNear", true);
                }
                break;
            case IAState.PlayerNear:
                if(!PlayerNear)
                {
                    _state = IAState.Patrol;
                    _animator.SetBool("IsPlayerNear", false);
                }
                break;
        }
    }
}

