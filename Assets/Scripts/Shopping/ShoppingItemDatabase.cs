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

    public void ResetPurchase()
    {
        for (int i = 0; i < shoppingItems.Length; i++)
        {
            shoppingItems[i].isPurchased = false;
        }
    }

    public ShoppingItem GetItem(int index)
    {
        return shoppingItems[index];
    }

    public void PurchaseItem(GameObject player, int index)
    {
        Debug.Log("Shopping Items purchase: " + shoppingItems[index].name);
        if(shoppingItems[index].isPurchased)
            return;
        shoppingItems[index].Purchase(player);
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