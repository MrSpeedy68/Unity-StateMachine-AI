using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ControlNPC : MonoBehaviour
{
    public GameObject target;
    public GameObject WP1, WP2, WP3, WP4;
    int WPCount;
    GameObject[] WayPoints;
    bool isWandering = false;
    bool isFollowingWayPoints = false;
    float timer;
    GameObject wanderingTarget;
    Animator anim;
    AnimatorStateInfo info;
    float patrolTimer;
    int nbWPReached = 0;
    Vector3 startingPosition;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("FPSController");
        patrolTimer = 0.0f;
        anim = GetComponent<Animator>();
        startingPosition = transform.position;
        target = new GameObject();
        if (isWandering)
        {
            wanderingTarget = new GameObject();
            wanderingTarget.transform.position = new Vector3(20, 0, 20);
            target = wanderingTarget;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
        WP1 = GameObject.Find("WP1");
        WP2 = GameObject.Find("WP2");
        WP3 = GameObject.Find("WP3");
        WP4 = GameObject.Find("WP4");
        WayPoints = new GameObject[] { WP1, WP2, WP3, WP4 };
        WPCount = 0;
        for (int i = 0; i < WayPoints.Length; i++)
        {
            WayPoints[i].GetComponent<Renderer>().enabled = false;
        }
    }
    void MoveToNextWP()
    {
        WPCount++;
        if (WPCount > WayPoints.Length - 1) WPCount = 0;
    }
    void MoveToRandomWP()
    {
        int previous = WPCount;
        int random = 0;
        do
        {
            random = Random.Range(0, WayPoints.Length);
        } while (random == previous);
        WPCount = random;
    }
    // Upda
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        listen();
        ///wander();
        //timer += Time.deltaTime;
        // if (timer > 4)
        // {
        //    timer = 0;
        //    wander();
        //}
        //if (isFollowingWayPoints)



        if (info.IsName("Idle"))
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer > 5)
            {
                patrolTimer = 0.0f;
                anim.SetTrigger("startPatrol");
            }
        }

        if (info.IsName("BackToStart"))
        {
            GetComponent<NavMeshAgent>().SetDestination(startingPosition);
            if (Vector3.Distance(transform.position, startingPosition) < 1.0f) anim.SetTrigger("hasReachedStart");
        }


        if (info.IsName("Patrol"))
        {
            target = WayPoints[WPCount];
            if (Vector3.Distance(transform.position, target.transform.position) < 1.0)
            {
                //MoveToNextWP();
                MoveToRandomWP();
                nbWPReached++;
                if (nbWPReached >= 4)
                {
                    nbWPReached = 0;
                    anim.SetTrigger("startToGoBackToStart");
                }
            }
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }

        if (info.IsName("FollowPlayer"))
        {
            target = player;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
    }

    void listen()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 3)
        {
            anim.SetBool("canHearPlayer", true);
        }
    }

    void look()
    {
        Ray ray = new Ray();
        RaycastHit hit;

    }


    void wander()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 0.7f;
        float distanceToObstacle = 0;
        float castingDistance = 20;
        do
        {
            float randomDirectionX = Random.Range(-1, 1);
            float randomDirectionZ = Random.Range(-1, 1);
            ray.direction = transform.forward * randomDirectionZ + transform.right * randomDirectionX;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
            {
                distanceToObstacle = hit.distance;
            }
            else distanceToObstacle = castingDistance;
            wanderingTarget.transform.position = ray.origin + ray.direction * (distanceToObstacle - 1);
            target = wanderingTarget;
        } while (distanceToObstacle < 1.0f);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
}
