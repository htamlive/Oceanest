using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "ShoppingItem", menuName = "Shopping/Shopping Item")]
[System.Serializable]
public class ShoppingItem : ScriptableObject
{
    public Sprite image;
    public string itemName;
    public string description;
    public int price;

    public bool isPurchased;

    public virtual void Purchase()
    {
        isPurchased = true;
    }
}


