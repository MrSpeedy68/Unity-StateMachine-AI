using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _ammo;
    [SerializeField] private int _collectableAmount = 0;
    
    private Vector3 _spawnLocation;

    private Weapon _weapon;
    // Start is called before the first frame update
    void Start()
    {
        _spawnLocation = transform.position;
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (_collectableAmount == 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Win");
        }

        if (_health <= 0f)
        {
            transform.position = _spawnLocation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            var medkit = other.GetComponent<Medkit>();
            AddHealth(medkit.healthAmount);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Ammo"))
        {
            var ammoBox = other.GetComponent<AmmoBox>();
            AddAmmo(ammoBox.boxAmmoAmount);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Collectable"))
        {
            _collectableAmount++;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            TakeDamage(5f);
        }
    }

    private void AddHealth(float health)
    {
        if (_health < 100f)
        {
            _health += health;
        }
    }

    private void AddAmmo(int ammoAmount)
    {
        _weapon.currentAmmo += ammoAmount;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
