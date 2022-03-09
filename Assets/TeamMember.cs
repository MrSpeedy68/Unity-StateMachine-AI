using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeamMember : MonoBehaviour
{
    private GameObject leader;
    private Animator anim;
    private AnimatorStateInfo info;
    private float _distanceToLeader;
    private NavMeshAgent _NavMeshAgent;
    private GameObject _target;
    private float _distanceToTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        leader = GameObject.Find("FPSController");
        _NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        
        _distanceToLeader = Vector3.Distance(leader.transform.position, transform.position);
        if (_distanceToLeader < 5.0f)
        {
            anim.SetBool("closeToLeader", true);
        }
        else anim.SetBool("closeToLeader", false);

        if (info.IsName("Idle"))
        {
            _NavMeshAgent.isStopped = true;
        }

        if (info.IsName("MoveTowardsLeader"))
        {
            _NavMeshAgent.SetDestination(leader.transform.position);
            _NavMeshAgent.isStopped = false;
        }

        if (info.IsName("GoToTarget"))
        {
            _NavMeshAgent.SetDestination(_target.transform.position);
            _NavMeshAgent.isStopped = false;
            
            _distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
            if (_distanceToTarget < 1.0f)
            {
                anim.SetBool("closeToTarget", true);
            }
            else anim.SetBool("closeToTarget", false);
        }

        if (info.IsName("AttackTarget"))
        {
            _NavMeshAgent.isStopped = true;
        }

    }

    public void Attack(GameObject t)
    {
        _target = t;
        anim.SetTrigger("attackOneToOne");
    }

    public void Retreat()
    {
        anim.SetTrigger("retreat");
    }
}