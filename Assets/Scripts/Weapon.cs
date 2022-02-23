using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int magazineSize;
    [SerializeField] private float fireRate;
    //[SerializeField] private Transform shootTransform;

    private int currentAmmo;
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fired");
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit,
                float.MaxValue))
            {
                Debug.Log(hit.collider.name);
            }
        }
        
    }


    public void Fire()
    {
        
    }
    
    
}
