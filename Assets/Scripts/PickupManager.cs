using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] ammo;
    public GameObject[] health;

    public Vector3 ReturnClosestHealth(Vector3 currentPos)
    {
        var closestHealth = ammo[0].transform.position;
        foreach (var h in health)
        {
            if (h != null && Vector3.Distance(currentPos, h.transform.position) < closestHealth.magnitude)
            {
                closestHealth = h.transform.position;
            }   
        }

        return closestHealth;
    }
    
    public Vector3 ReturnClosestAmmo(Vector3 currentPos)
    {
        var closestAmmo = ammo[0].transform.position;
        foreach (var a in ammo)
        {
            if (a != null && Vector3.Distance(currentPos, a.transform.position) < closestAmmo.magnitude)
            {
                closestAmmo = a.transform.position;
            }            
        }

        return closestAmmo;
    }
}
