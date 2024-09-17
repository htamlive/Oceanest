using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpacing = .5f;
    float itemHeight;

    [Header("UI elements")]
    [SerializeField] Image selectedCharacterIcon;
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject[] disableCoinViews;
    [Space(20)]
    [SerializeField] ShoppingItemDatabase shoppingItemDB;

    [Space(20)]
    [Header("Shop Events")]
    [SerializeField] GameObject shopUI;
    //[SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] Button scrollUpButton;

    [Space(20)]
    [Header("Main Menu")]
    [SerializeField] Image mainMenuCharacterImage;
    [SerializeField] TMP_Text mainMenuCharacterName;

    [Space(20)]
    [Header("Scroll View")]
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject topScrollFade;
    [SerializeField] GameObject bottomScrollFade;

    [Space(20)]
    [Header("Purchase Fx & Error messages")]
    [SerializeField] ParticleSystem purchaseFx;
    [SerializeField] Transform purchaseFxPos;
    [SerializeField] TMP_Text noEnoughCoinsText;

    [Space(20)]
    [SerializeField] GameObject mobileButton;

    [SerializeField]
    GameObject player;

    int newSelectedItemIndex = 0;
    int previousSelectedItemIndex = 0;

    bool isInSandBox = false;

    void Start()
    {
        //Move Fx to the exact same position of the coin image (for different screen sizes)
        //purchaseFx.transform.position = purchaseFxPos.position;
        
        AddShopEvents();
        GenerateShopItemsUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool isActive = shopUI.activeInHierarchy;
            SetShopStatus(!isActive);
        }
    }


    void GenerateShopItemsUI()
    {
        ////Loop throw save purchased items and make them as purchased in the Database array
        for (int i = 0; i < GameDataManager.GetAllPurchasedItem().Count; i++)
        {
            int purchasedCharacterIndex = GameDataManager.GetPurchasedItem(i);
            shoppingItemDB.MarkPurchased(purchasedCharacterIndex);
        }

        ////Delete itemTemplate after calculating item's Height :
        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        //DetachChildren () will make sure to delete it from the hierarchy, otherwise if you 
        //write ShopItemsContainer.ChildCount you w'll get "1"
        ShopItemsContainer.DetachChildren();

        //Debug.Log("Item count : " + shoppingItemDB.ItemsCount);

        for (int i = 0; i < shoppingItemDB.ItemsCount; i++)
        {
            //Create a Character and its corresponding UI element (uiItem)
            ShoppingItem item = shoppingItemDB.GetItem(i);
            ShoppingItemUI uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<ShoppingItemUI>();

            //Move item to its position
            uiItem.SetItemPosition((itemHeight + itemSpacing) * i * Vector2.down);

            //Set Item name in Hierarchy (Not required)
            uiItem.gameObject.name = "Item" + i + "-" + item.name;

            //Add information to the UI (one item)
            uiItem.SetCharacterName(item.itemName);
            uiItem.SetCharacterImage(item.image);
            //uiItem.SetCharacterSpeed(character.speed);
            //uiItem.SetCharacterPower(character.power);
            uiItem.SetCharacterDescription(item.description);
            uiItem.SetCharacterPrice(item.price);

            if (item.isPurchased)
            {
                uiItem.SetItemAsPurchased();
                //uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                uiItem.OnItemPurchase(i, OnItemPurchased);
            }

            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                Vector2.up * ((itemHeight + itemSpacing) * shoppingItemDB.ItemsCount + itemSpacing);

            //you can use VerticalLayoutGroup with ContentSizeFitter to skip all of this :
            //(moving items & resizing the container)
        }

    }

    ShoppingItemUI GetItemUI(int index)
    {
        return ShopItemsContainer.GetChild(index).GetComponent<ShoppingItemUI>();
    }

    void OnItemPurchased(int index)
    {
        ShoppingItem shoppingItem = shoppingItemDB.GetItem(index);
        ShoppingItemUI uiItem = GetItemUI(index);

        if (GameDataManager.CanSpendCoins(shoppingItem.price))
        {
            GameDataManager.SpendCoins(shoppingItem.price);

            purchaseFx.Play();

            GameSharedUI.Instance.UpdateCoinsUIText();

            //Update DB's Data
            shoppingItemDB.PurchaseItem(player, index);

            if (shoppingItemDB.IsPurchased(index))
            {
                uiItem.SetItemAsPurchased();
                GameDataManager.AddPurchasedCharacter(index);
            }
        }
        else
        {
            AnimateNoMoreCoinsText();
            uiItem.AnimateShakeItem();
        }
    }

       void AnimateNoMoreCoinsText() 
    { 
        // Complete animations (if it's running)
        noEnoughCoinsText.transform.DOComplete();
        noEnoughCoinsText.DOComplete();

        noEnoughCoinsText.transform.DOShakePosition(3f, new Vector3(5f, 0f, 0f), 10, 0);
        noEnoughCoinsText.DOFade(1f, 3f).From(0f).OnComplete(() =>
        {
            noEnoughCoinsText.DOFade(0f, 1f);
        });

    }

    void AddShopEvents()
    {
        //openShopButton.onClick.RemoveAllListeners();
        //openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);

        scrollRect.onValueChanged.RemoveAllListeners();
        scrollRect.onValueChanged.AddListener(OnShopListScroll);

        //scrollUpButton.onClick.RemoveAllListeners();
        //scrollUpButton.onClick.AddListener(OnScollUpClicked);
    }

    void CloseShop()
    {
        SetShopStatus(false);
    }

    void OnScollUpClicked()
    {
        scrollRect.DOVerticalNormalizedPos(1f, .5f).SetEase(Ease.OutBack);
    }

    void OnShopListScroll(Vector2 value)
    {
        float scrollY = value.y;
        Debug.Log("Scroll Y : " + scrollY);

        if (scrollY < 1f)
            topScrollFade.SetActive(true);
        else
            topScrollFade.SetActive(false);

        if (scrollY > 0f)
            bottomScrollFade.SetActive(true);
        else
            bottomScrollFade.SetActive(false);
    }



    void SetShopStatus(bool isOpening)
    {
        shopUI.SetActive(isOpening);
        foreach (var item in disableCoinViews)
        {
            item.SetActive(!isOpening);
        }
    }


}