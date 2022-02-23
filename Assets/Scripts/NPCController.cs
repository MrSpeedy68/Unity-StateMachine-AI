using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public List<GameObject> wayPoints;
    public float hearingDistance;

    private Animator _animator;
    private bool _isPatroling = true;
    private AnimatorStateInfo _animationStateInfo;
    private int _WPCount = 0;
    private GameObject _player;
    private NavMeshAgent _NavMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.Find("FPSController");
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _animationStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        
        //Hearing
        
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance <= hearingDistance)
        {
            _animator.SetBool("canHearPlayer",true);
        }
        else _animator.SetBool("canHearPlayer",false);

        if (_animationStateInfo.IsName("FollowPlayer"))
        {
            _NavMeshAgent.SetDestination(_player.transform.position);
        }
        
        //Patrolling
        
        if (_animationStateInfo.IsName("Patrol"))
        {
            var target = wayPoints[_WPCount];
            if (Vector3.Distance(transform.position, target.transform.position) < 1.0)
            {
                MoveToNextWP();
            }

            _NavMeshAgent.SetDestination(target.transform.position);
        }
        

    }

    private void MoveToNextWP()
    {
        _WPCount++;
        if (_WPCount > wayPoints.Count - 1)
        {
            _WPCount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,hearingDistance/2);
    }
}
