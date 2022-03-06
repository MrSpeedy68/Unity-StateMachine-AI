using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public GameObject[] ammo;

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
