using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
#if UNITY_ANALYTICS
using UnityEngine.Analytics;

#endif

public class ShopUI : MonoBehaviour
{

    public Button BackBtn;

    public ShopCharacterList characterList;


    [Header("UI")]
    public Text coinCounter;
    public Text premiumCounter;

    public GameObject ShopGame;

    protected ShopList m_OpenList;

    protected const int k_CheatCoins = 1000000;
    protected const int k_CheatPremium = 1000;


	void Start ()
    {
      //  BackBtn.onClick.AddListener(CloseShop);

    }
	
	void Update ()
    {
        //   coinCounter.text = PlayerData.instance.coins.ToString();
       
    }

    void CloseShop()
    {
        //Output this to console when the Button is clicked
        ShopGame.SetActive(false);

    }
    public void OpenItemList()
    {
        m_OpenList.Close();
      
       
    }

    public void OpenCharacterList()
    {
        m_OpenList.Close();
        characterList.Open();
        m_OpenList = characterList;
    }

    public void OpenThemeList()
    {
        m_OpenList.Close();
       
    }

    public void OpenAccessoriesList()
    {
        m_OpenList.Close();
       
    }



}
