using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBreadCrumb : MonoBehaviour
{
    private Vector3 _previousPosition;
    private float _counter = 0;
    public GameObject BC;
    
    void Start()
    {
        _previousPosition = transform.position;
    }
    
    void Update()
    {
        BreadCrumbs();
    }


    private void BreadCrumbs()
    {
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(_previousPosition, currentPosition);
        if (distance > 1.0f)
        {
            _previousPosition = currentPosition;
            GameObject g = Instantiate(BC, currentPosition, Quaternion.identity);
            g.name = "BC" + _counter;
            g.gameObject.GetComponent<MeshRenderer>().material.color =
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            _counter++;
            Destroy(g,10f);
        }
    }
}
