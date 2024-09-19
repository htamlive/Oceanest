using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShoppingItem", menuName = "Shopping/Tracking Missiles Item")]
[System.Serializable]
public class TrackingMissiles : ShoppingItem
{
    public override void Purchase(GameObject player)
    {
        isPurchased = true;
        GameDataManager.UpdateTrackingMissilesStatus(player, true);
    }
}
