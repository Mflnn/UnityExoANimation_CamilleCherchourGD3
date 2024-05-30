using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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

    [SerializeField] private IAState _state = IAState.None;
    [SerializeField] private Animator _animator;
    public bool PlayerNear = false;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _waypoint1;
    [SerializeField] private GameObject _waypoint2;
    [SerializeField] private GameObject _waypoint3;
    [SerializeField] private GameObject _waypoint4;
    [SerializeField] private GameObject _waypoint5;
    [SerializeField] private float _speed;
    public bool PlayerSeen = false;

    private void Start()
    {
        _state = IAState.Idle;
    }


    private void Update()
    {
        CheckTransition();
        Behavior();
        Debug.Log(_state);
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
                _agent.SetDestination(_waypoint2.transform.position);
                _speed = _agent.velocity.magnitude;
                _animator.SetFloat("Speed", _speed);
                Debug.Log(_waypoint1.transform.position);
                Debug.Log("StatePatrol");
                break;
            case IAState.PlayerSeen:
                Debug.Log("StatePlayerSeen");
                _agent.SetDestination(_waypoint1.transform.position);
                break;
            case IAState.PlayerNear:
                Debug.Log("StatePlayerNear");
                _agent.SetDestination(_waypoint4.transform.position);
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
                if (PlayerNear & _speed>0 & PlayerSeen)
                {
                    _state = IAState.PlayerNear;
                    _animator.SetBool("IsPlayerNear", true);
                    _animator.SetBool("IsPlayerSeen", true) ;
                }
                else if (PlayerSeen & _speed>0)
                {
                    _state = IAState.PlayerSeen;
                    _animator.SetBool("IsPlayerSeen", true);
                }
                else 
                    _state = IAState.Patrol;
                break;
            case IAState.Patrol:
                if (PlayerNear & PlayerSeen)
                {
                    _state = IAState.PlayerNear;
                    _animator.SetBool("IsPlayerNear", true);
                    _animator.SetBool("IsPlayerSeen", true);
                }
                else if (PlayerSeen)
                {
                    _state = IAState.PlayerSeen;
                    _animator.SetBool("IsPlayerSeen", true);
                }
                else if (_speed <= 0 & !PlayerSeen & !PlayerNear)
                {
                    _state = IAState.Idle;
                }
                break;
            case IAState.PlayerSeen:
                if (PlayerNear)
                {
                    _state = IAState.PlayerNear;
                    _animator.SetBool("IsPlayerNear", true);
                    _animator.SetBool("IsPlayerSeen", true);
                }
                else if (!PlayerSeen & _speed>0)
                {
                    _state = IAState.Patrol;
                    _animator.SetBool("IsPlayerSeen", false);
                }
                break;
            case IAState.PlayerNear:
                if (!PlayerNear & PlayerSeen)
                {
                    _state = IAState.PlayerSeen;
                    _animator.SetBool("IsPlayerNear", false);
                }
                else if (!PlayerNear & !PlayerSeen)
                {
                    _state = IAState.Patrol;
                    _animator.SetBool("IsPlayerNear", false);
                    _animator.SetBool("IsPlayerSeen", false);
                }
                break;
        }
    }
}

