using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StorageManager : InventorySystem
{
    new public static StorageManager Instance;

    public StorageUnit currentStorageUnit;

    public GameObject inventoryMenu;

    //when this menu opens, the current menuopen is storage menu, so it wont instantiate objects into the world
    //we will override that method for moving items to the other inventory system

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        //create the grid
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    // Update is called once per frame
    internal override void Update()
    {
        //menu is opened by clicking storage unit but closed with tab
        if(CurrentMenuIsThis() && Input.GetKeyUp(KeyCode.Tab))
        {
            Debug.Log("storage manager toggle menu");
            //close the menu
            ToggleMenu();
        }
        return;
    }

    public override void OpenMenuFunctions()
    {
        //open the inventory gameobject when storage is open
        inventoryMenu.SetActive(true);
    }

    public override void CloseMenuFunctions()
    {
        //close the inventory gameobject
        inventoryMenu.SetActive(false);
    }

    public void SetStorage(InteractableObject hoverObject)
    {
        currentStorageUnit = hoverObject.GetComponent<StorageUnit>();
        itemsList = currentStorageUnit.storedItems;
        SortItems();
    }

    public override void MoveItem(ItemScriptableObject item)
    {
        if (!InventorySystem.Instance.AddItem(item))
        {
            Debug.Log("doesn't fit!");
            return;
        }

        //if it worked,
        RemoveItem(item);
    }

    internal override bool SortItems()
    {
        //Debug.Log("SortItems");

        //sort items by size
        var sortedList = itemsList.OrderByDescending(s => s.size.x * s.size.y);

        //place items systematically
        foreach (ItemScriptableObject item in sortedList)
        {
            bool hasSpot = AvailSpot(item);
            if (hasSpot == false)
            {
                Debug.Log("doesnt fit!");
                ResetTempValues();
                return false;
            }
        }

        foreach (GridObject obj in grid.gridArray)
        {
            obj.SetTempAsReal(Instance);
        }

        return true;
    }
}
