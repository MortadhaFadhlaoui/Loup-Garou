using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_ANALYTICS
using UnityEngine.Analytics;

#endif

public class ShopCharacterList : ShopList
{
    public Sprite f1, f2;
    public bool b1 = false, b2 = false;
    public override void Populate()
    {

        m_RefreshCallback = null;
        foreach (Transform t in listRoot)
        {
            Destroy(t.gameObject);
        }
        
        Character c1 = new Character();
        c1.cost = 400;
        c1.characterName = "BANTOUF";
        c1.icon = f1;

        Character c2 = new Character();
        c2.cost = 300;
        c2.characterName = "BG";
        c2.icon = f2;
        List<Character> list = new List<Character>();
        list.Add(c1);
        list.Add(c2);
      
        Debug.Log(list.Count);
        foreach (Character c in list)
        {
            if (PlayerPrefs.GetInt("c1") == 1)
            {
                b1 = true;
            }
            if (PlayerPrefs.GetInt("c2") == 1)
            {
                b2 = true;
            }
            Debug.Log(b1 + " b2 :" + b2);
            GameObject newEntry = Instantiate(prefabItem);
            newEntry.transform.SetParent(listRoot, false);

            ShopItemListItem itm = newEntry.GetComponent<ShopItemListItem>();

            //  itm.icon.sprite = c.icon;
            itm.nameText.text = c.characterName;
            itm.pricetext.text = c.cost.ToString();
            itm.icon.sprite = c.icon;
            if (c.characterName.Equals("BANTOUF"))
            {
                if (b1 == true)
                {
                    Debug.Log("mechri");
                    itm.buyButton.transform.GetComponentsInChildren<Text>()[0].text = "purchased";
                    itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize = itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize - 3;
                    itm.buyButton.image.sprite = itm.disabledButtonSprite;
                }
                else
                {
                    itm.buyButton.image.sprite = itm.buyButtonSprite;
                }
            }
            if (c.characterName.Equals("BG"))
            {
                if (b2 == true)
                {
                    itm.buyButton.transform.GetComponentsInChildren<Text>()[0].text = "purchased";
                    itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize = itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize - 3;
                    itm.buyButton.image.sprite = itm.disabledButtonSprite;
                }
                else
                {
                    itm.buyButton.image.sprite = itm.buyButtonSprite;
                }
            }

            if (c.premiumCost > 0)
            {
                itm.premiumText.transform.parent.gameObject.SetActive(true);
                itm.premiumText.text = c.premiumCost.ToString();
            }
            else
            {
                itm.premiumText.transform.parent.gameObject.SetActive(false);
            }

            itm.buyButton.onClick.AddListener(delegate () { Buy(c,itm); });
        }

    }
    public void Buy(Character c, ShopItemListItem itm)
    {
        
        int f = PlayerPrefs.GetInt("solde");
        

        Debug.Log(c.characterName);
        if (c.characterName.Equals("BG") && PlayerPrefs.GetInt("c2") == 0)
        {
            if(f> c.cost)
            {
                itm.buyButton.image.sprite = itm.disabledButtonSprite;
                itm.buyButton.transform.GetComponentsInChildren<Text>()[0].text = "purchased";
                itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize = itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize - 3;
                PlayerPrefs.SetInt("c2", 1);
                PlayerPrefs.SetInt("solde", f-c.cost);
                Debug.Log(PlayerPrefs.GetInt("solde"));
            }
            
        }
        if (c.characterName.Equals("BANTOUF") && PlayerPrefs.GetInt("c1") == 0)
        {
            if (f > c.cost)
            {
                itm.buyButton.image.sprite = itm.disabledButtonSprite;
                itm.buyButton.transform.GetComponentsInChildren<Text>()[0].text = "purchased";
                itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize = itm.buyButton.transform.GetComponentsInChildren<Text>()[0].fontSize - 3;
                PlayerPrefs.SetInt("c1", 1);
                PlayerPrefs.SetInt("solde", f - c.cost);
            }
        }
            
    
    }
}
