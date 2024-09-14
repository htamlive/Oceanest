using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{

    [SerializeField] private float healthBarWidthEased;
    [SerializeField] private float smoothFactor = 5f;
    void Start()
    {
        healthBarWidthEased = 1;
    }

    public void UpdateHealthBar()
    {
        //Debug.Log("Updating health bar " + player.health);
        var playerData = GameDataManager.GetPlayerData();
        float healthBarWidth = (float)playerData.health / (float)playerData.maxHealth;

        //Debug.Log("Player index, health, max health: " + player.PlayerIndex + ", " + player.health + ", " + player.maxHealth);

        healthBarWidthEased += (healthBarWidth - healthBarWidthEased) * Time.deltaTime * smoothFactor;

        transform.localScale = new Vector2(healthBarWidthEased, 1);
    }
}
