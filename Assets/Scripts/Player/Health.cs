using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    public static ParticlePooler pp;

    public delegate void DieAction();
    public DieAction die;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        die = DefaultDie;
    }

    public void RestoreHealth(int restoreAmount)
    {
        //make sure health cannot exceed max
        currentHealth = Mathf.Min(currentHealth += restoreAmount, maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        //play particle effect
        ParticleSystem particle = pp.GetParticle();
        particle.transform.position = transform.position;
        particle.Play();

        currentHealth -= damageAmount;
        CheckDead();
    }

    public void CheckDead()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            //die
            die();
        }
    }

    public void DefaultDie()
    {
        //get destroyed
        Debug.Log(name + " died!");
        Destroy(gameObject);
    }
}
