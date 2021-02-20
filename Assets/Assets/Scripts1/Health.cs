using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour


{
    public PlayerFollow pf;
    public int health = 25;
    public GameObject deathEffect;
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(health);
    }

    public void takeDamage(int damage)
    {
        if (health-damage > 0)
        {
            health = health - damage;
            healthbar.setHealth(health);
            Debug.Log(health);
        }
        else
        {
            StartCoroutine(die());
        }

    }
    IEnumerator die()
    {
        Instantiate(deathEffect,transform.position ,Quaternion.identity);
        Destroy(gameObject);
        healthbar.setHealth(0);
        StartCoroutine(pf.destroyInstance());
        yield return new WaitForSeconds(0.5f);
       
        
    }
}
