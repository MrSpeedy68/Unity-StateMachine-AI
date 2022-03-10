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
        allTargets = GameObject.FindGameObjectsWithTag("AI");

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
        int numTargets = 0;
        if (allTargets != null)
        {
            for (int i = 0; i < nbTeamMembers; i++)
            {
                Debug.Log(numTargets);
                if (numTargets < nbTargets)
                {
                    teamMembers[i].Attack(allTargets[numTargets]);
                    numTargets++;
                }
                else
                {
                    numTargets = 0;
                    teamMembers[i].Attack(allTargets[numTargets]);
                    numTargets++;
                }
            
            }
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
