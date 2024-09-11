using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class ShoppingItemUI : MonoBehaviour
{
    [SerializeField] Color itemNotSelectedColor;
    [SerializeField] Color itemSelectedColor;

    [Space(20f)]
    [SerializeField] Image shoppingItemImage;
    [SerializeField] TMP_Text itemNameText;
    //[SerializeField] Image characterSpeedFill;
    //[SerializeField] Image characterPowerFill;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Button purchaseButton;

    [Space(20f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemImage;
    //[SerializeField] Outline itemOutline;

    //--------------------------------------------------------------
    public void SetItemPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    public void SetCharacterImage(Sprite sprite, float scaleRation = 1f)
    {
        shoppingItemImage.sprite = sprite;
        //shoppingItemImage.rectTransform.localScale = Vector3.one * scaleRation;
    }

    public void SetCharacterName(string name)
    {
        itemNameText.text = name;
    }

    //public void SetCharacterSpeed(float speed)
    //{
    //    characterSpeedFill.fillAmount = speed / 100;
    //}

    //public void SetCharacterPower(float power)
    //{
    //    characterPowerFill.fillAmount = power / 100;
    //}

    public void SetCharacterDescription(string desc)
    {
        description.text = desc;
    }

    public void SetCharacterPrice(int price)
    {
        priceText.text = price.ToString();
    }

    public void SetItemAsPurchased()
    {
        purchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;

        itemImage.color = itemNotSelectedColor;
        SelectItem();
    }

    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    //public void OnItemSelect(int itemIndex, UnityAction<int> action)
    //{
    //    itemButton.interactable = true;

    //    itemButton.onClick.RemoveAllListeners();
    //    itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
    //}

    public void SelectItem()
    {
        //itemOutline.enabled = true;
        itemImage.color = itemSelectedColor;
        itemButton.interactable = false;
    }

    //public void DeselectItem()
    //{
    //    itemOutline.enabled = false;
    //    itemImage.color = itemNotSelectedColor;
    //    itemButton.interactable = true;
    //}

    public void AnimateShakeItem()
    {
        //End all animations first
        transform.DOComplete();

        transform.DOShakePosition(1f, new Vector3(8f, 0, 0), 10, 0).SetEase(Ease.Linear);
    }
}
