using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushZone : MonoBehaviour
{
    public bool isPlayerPresent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerPresent = true;
            StartCoroutine("Timer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerPresent = false;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        isPlayerPresent = false;
    }
}
