using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] ammo;
    public GameObject[] health;

    public bool IsAmmoAvailable()
    {
        return CheckAvailability(ammo);
    }

    public bool IsHealthAvailable()
    {
        return CheckAvailability(health);
    }

    private bool CheckAvailability(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null) return true;
        }

        return false;
    }

    public Vector3 ReturnClosestHealth(Vector3 currentPos)
    {
        return ReturnClosestObject(currentPos, health);
    }
    
    public Vector3 ReturnClosestAmmo(Vector3 currentPos)
    {
        return ReturnClosestObject(currentPos, ammo);
    }

    private Vector3 ReturnClosestObject(Vector3 currentPos, GameObject[] arr)
    {
        Vector3 closestObj = new Vector3(float.MaxValue,float.MaxValue,float.MaxValue);
        foreach (var a in arr)
        {
            if (a != null && Vector3.Distance(currentPos, a.transform.position) < closestObj.magnitude)
            {
                closestObj = a.transform.position;
            }            
        }

        return closestObj;
    }
    
    
}
