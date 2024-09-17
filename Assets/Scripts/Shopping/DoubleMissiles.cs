using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShoppingItem", menuName = "Shopping/Double Missiles Item")]
[System.Serializable]
public class DoubleMissiles : ShoppingItem
{
    public override void Purchase(GameObject player)
    {
        isPurchased = true;
        GameDataManager.UpdateDoubleMissilesStatus(player, true);
    }
}
