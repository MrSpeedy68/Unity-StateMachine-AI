using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public float healthAmount;


    public void Destroy()
    {
        Destroy(gameObject);
    }
}