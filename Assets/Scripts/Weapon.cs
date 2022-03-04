using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int magazineSize;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private int currentAmmo;
    [SerializeField] private Camera cam;
    
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
            Fire();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
        
    }


    private void Fire()
    {
        currentAmmo--;
        
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out RaycastHit hit,
            float.MaxValue))
        {
            if (hit.collider.gameObject.CompareTag("AI"))
            {
                Debug.Log("Hit AI");
                var AI = hit.collider.GetComponent<NPCController>();
                AI.TakeDamage(damage);
            }
        }
    }

    private void Reload()
    {
        currentAmmo = magazineSize;
    }
    
    
    
    
}
