using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public List<GameObject> WayPoints;

    private Animator _animator;
    private bool _isPatroling = true;
    AnimatorStateInfo animationStateInfo;
    private int WPCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animationStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        
        if (animationStateInfo.IsName("Patrol"))
        {
            var target = WayPoints[WPCount];
            if (Vector3.Distance(transform.position, target.transform.position) < 1.0)
            {
                MoveToNextWP();
            }

            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
    }

    private void MoveToNextWP()
    {
        WPCount++;
        if (WPCount > WayPoints.Count - 1)
        {
            WPCount = 0;
        }
    }
}
