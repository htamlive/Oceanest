using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private float healthBarWidthEased;
    [SerializeField] private float smoothFactor = 5f;

    public WormManager wormManager;
    void Start()
    {
        healthBarWidthEased = 1;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        //Debug.Log("Updating health bar " + player.health);
        float healthBarWidth = wormManager.GetHealthPercent();


        healthBarWidthEased += (healthBarWidth - healthBarWidthEased) * Time.deltaTime * smoothFactor;

        transform.localScale = new Vector2(healthBarWidthEased, 1);
    }
}
