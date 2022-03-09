using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    public TeamMember[] teamMembers;
    public GameObject[] allTargets;
    
    
    private int nbTeamMembers, nbTargets;
    
    // Start is called before the first frame update
    void Start()
    {
        nbTeamMembers = teamMembers.Length;
        nbTargets = allTargets.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Retreat();
        }
    }

    void Attack()
    {
        for (int i = 0; i < nbTargets; i++)
        {
            teamMembers[i].Attack(allTargets[i]);
        }
    }

    void Retreat()
    {
        for (int i = 0; i < nbTeamMembers; i++)
        {
            teamMembers[i].Retreat();
        }
    }
}
