using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.WSA;

[RequireComponent(typeof(RecoveryCounter))]
public class SubmarineStat : MonoBehaviour
{
    public bool HasTakenDamage { get; private set; }
    public RecoveryCounter recoveryCounter;
    private int health;
    private int maxHealth;
    public bool dead;

    // Start is called before the first frame update
    void Start()
    {
        
        var playerData = GameDataManager.GetPlayerData();
        health = playerData.health;
        maxHealth = playerData.maxHealth;
        var position = playerData.position;
        var rotation = playerData.rotation;
        transform.SetPositionAndRotation(new Vector3(position[0], position[1], position[2]), new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]));
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.C))
        {
            StartCoroutine(Die());
        }
#endif

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.B))
        {
            Damage(50);
        }
#endif

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.X))
        {
            GameDataManager.AddCoins(100);

        }
#endif




    }

    public void Damage(int damageAmount, int direction = 1)
    {
        //Debug.Log("Taken Damage: " + damageAmount + " Player: " + playerIndex);
        HasTakenDamage = true;
        if (!recoveryCounter.recovering)
        {
            //effects.DamageEffect();
            recoveryCounter.counter = 0;
            //launch = direction * (hurtLaunchPower.x);
            GameDataManager.AddHealth(-damageAmount);
            health = GameDataManager.GetHealth();
            if (health <= 0)
            {
                //gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
                StartCoroutine(Die());
            }
            GameManager.Instance.hud.HealthBarHurt();
        }
    }

    public void ResetLevel()
    {
        Debug.Log("Player requested to reset level");
        dead = false;
        GameDataManager.SetHealth(maxHealth);
        health = maxHealth;
    }

    public IEnumerator Die()
    {
        dead = true;
        //Hide(true);
        //effects.DieEffect();
        //UpdateDie();
        GameManager.Instance.ResetGamePlay();
        yield return null;
    }
}
