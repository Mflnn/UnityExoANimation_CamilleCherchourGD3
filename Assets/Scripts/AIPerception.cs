using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIPerception : MonoBehaviour
{

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Pawn;
    private Vector3 checkDirection;
    [SerializeField] private float distance;
    public SphereCollider SphereCollider;
    [SerializeField] private Animator _animator;

    private void CheckDistance()
    {
        checkDirection = Player.transform.position - Pawn.transform.position;
        checkDirection = checkDirection.normalized;
        RaycastHit hit;
        Debug.DrawLine(Pawn.transform.position, Pawn.transform.position + checkDirection * distance);

        if (Physics.Raycast(Pawn.transform.position, checkDirection, out hit, distance))
        {
            if (hit.collider.gameObject.GetComponent<PlayerController>())
            {
                Pawn.GetComponentInChildren<AIController>().PlayerNear = true;
                Debug.Log("player near");
            }
            else
            {
                Pawn.GetComponentInChildren<AIController>().PlayerNear = false;
            }
        }
        else
        {
            Pawn.GetComponentInChildren<AIController>().PlayerNear = false;
        }
    }

    private void Update()
    {
        CheckDistance();
    }
    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool("IsPlayerSeen", true);
        Debug.Log("playerseen");
    }

}
