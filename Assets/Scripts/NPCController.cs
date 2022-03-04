using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public List<GameObject> wayPoints;
    public float hearingDistance;
    public float sightDistance;
    public float smellDuration;

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
        
        _animator.SetTrigger("startPatrol");
    }

    // Update is called once per frame
    void Update()
    {
        _animationStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        
        Listen();
        Sight();

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
        
        //Smell Counter
        if (smellDuration > 0f)
        {
            smellDuration -= Time.deltaTime;
        }
        else _animator.SetBool("canSmellPlayer", false);
    }

    private void Sight()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 1.5f;
        string objectInSight;

        ray.direction = transform.forward * sightDistance;
        Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, sightDistance))
        {
            objectInSight = hit.collider.gameObject.name;
            if (objectInSight == "FPSController")
            {
                _animator.SetBool("canSeePlayer",true);
            }
            else
            {
                _animator.SetBool("canSeePlayer",false);
            }
        }
        else _animator.SetBool("canSeePlayer",false);
    }

    private void Listen()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance <= hearingDistance)
        {
            _animator.SetBool("canHearPlayer",true);
        }
        else _animator.SetBool("canHearPlayer",false);
    }

    private void Smell()
    {
        smellDuration = 3f;
        _animator.SetBool("canSmellPlayer", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BreadCrumb"))
        {
            Smell();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BreadCrumb"))
        {
            Debug.Log("Foiund Crumb fam");
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
