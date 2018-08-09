using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class voyantescript : MonoBehaviour
{

    public Text Name;
    public Toggle Checked;
    public float MyTime = 0f;
    private Image ProgressLoader;


    // Use this for initialization
    void Start()
    {
        ProgressLoader = GameObject.FindGameObjectWithTag("CirclePointerImage").GetComponent<Image>();
        Debug.Log(ProgressLoader.name);

    }

    // Update is called once per frame
    void Update()
    {

        MyTime += Time.deltaTime;
        ProgressLoader.fillAmount = MyTime / 3;

        if (MyTime >= 3f)
        {
            Debug.Log("feeeeet");
            OnFocusItem();
        }

    }

    public void OnFocusItem()
    {
        Debug.Log("mfocsi");
        string ff = "";
        PhotonPlayer[] listpl = PhotonNetwork.playerList;
        foreach (var item in listpl)
        {
            if (item.ID == int.Parse(Name.name))
            {
                ff = item.CustomProperties["Role"] as string;

            }
        }
        GameObject fff = GameObject.FindGameObjectWithTag("voyantetext");
        if (ff.Equals("L"))
        {
            ff = "Loup";
        }
        else if (ff.Equals("V"))
        {
            ff = "vilagois";
        }
        else if (ff.Equals("VSORCIERE"))
        {
            ff = "Sorciére";
        }
        else
        {
            ff = "Salvateur";
        }

        fff.GetComponent<Text>().text = ff;
        
        GameObject.FindGameObjectWithTag("playercont").GetComponent<Image>().color = new Color(255,255,255,0) ;
        StartCoroutine(hidePaper());




        //VoteManager. // awake // addvote // checkifvotecompleted // 
    }

    public void HelloFocus()
    {
        Debug.Log("HelloFocus !");
        GetComponent<voyantescript>().enabled = true;
    }

    public void ResetFocus()
    {
        MyTime = 0f;
        GetComponent<voyantescript>().enabled = false;
        ProgressLoader.fillAmount = MyTime / 3;


    }

    private IEnumerator hidePaper()
    {
        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("playercont").GetComponent<Image>().color = new Color(255, 255, 255, 100);
        GameObject.FindGameObjectWithTag("papercanvas").SetActive(false);
        PlayerNetwork.bvo = true;
        PlayerNetwork.Instance.setVoyantTue();
    }
}
