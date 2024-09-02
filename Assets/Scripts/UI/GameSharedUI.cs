using UnityEngine;
using TMPro;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] TMP_Text[] coinsUIText;
    float coinEased;

    void Start()
    {
        UpdateCoinsUIText();
        //coinEased = GameDataManager.GetCoins();
    }

    public void UpdateCoinsUIText()
    {
        //for (int i = 0; i < coinsUIText.Length; i++)
        //{
        //    SetCoinsText(coinsUIText[i], GameDataManager.GetCoins());
        //}
    }

    private void Update()
    {
        //coinEased += (GameDataManager.GetCoins() - coinEased) * Time.deltaTime * 5f;
        //for (int i = 0; i < coinsUIText.Length; i++)
        //{
        //    SetCoinsText(coinsUIText[i], GameDataManager.GetCoins());
        //}

        //if (coinEased >= GameDataManager.GetCoins())
        //{
        //    //animator.SetTrigger("getGem");
        //    coinEased = GameDataManager.GetCoins() + 1;
        //}
    }

    void SetCoinsText(TMP_Text textMesh, int value)
    {
        // if (value >= 1000000)...
        // .....

        if (value >= 1000)
            textMesh.text = string.Format("{0}K.{1}", (value / 1000), GetFirstDigitFromNumber(value % 1000));
        else
            textMesh.text = value.ToString();
    }

    int GetFirstDigitFromNumber(int num)
    {
        return int.Parse(num.ToString()[0].ToString());
    }
}