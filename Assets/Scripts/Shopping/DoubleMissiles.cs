using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShoppingItem", menuName = "Shopping/Double Missiles Item")]
[System.Serializable]
public class DoubleMissiles : ShoppingItem
{
    public override void Purchase()
    {
        isPurchased = true;
        GameDataManager.DoubleMissilesStatus = true;
    }
}
