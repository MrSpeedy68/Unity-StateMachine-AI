using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public GameObject explosion;
    private float timer;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3f)
        {
            var exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject,5f);
            Destroy(exp,5f);
        }
    }
}
