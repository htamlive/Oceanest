using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "ShopDatabase", menuName = "Shopping/Shop database")]
public class ShoppingItemDatabase : ScriptableObject
{
    public ShoppingItem[] shoppingItems;

    public int ItemsCount
    {
        get { return shoppingItems.Length; }
    }

    public ShoppingItem GetItem(int index)
    {
        return shoppingItems[index];
    }

    public void PurchaseItem(int index)
    {
        Debug.Log("Shopping Items purchase: " + shoppingItems[index].name);
        if(shoppingItems[index].isPurchased)
            return;
        shoppingItems[index].Purchase();
    }

    public void MarkPurchased(int index)
    {
        shoppingItems[index].isPurchased = true;
    }

    public bool IsPurchased(int index)
    {
        return shoppingItems[index].isPurchased;
    }
}