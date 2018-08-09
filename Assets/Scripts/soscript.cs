using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soscript : MonoBehaviour
{

    public Text Name;
    public Toggle Checked;
    public float MyTime = 0f;
    private Image ProgressLoader;


    // Use this for initialization
    void Start()
    {
        ProgressLoader = GameObject.FindGameObjectWithTag("CirclePointerImage").GetComponent<Image>();

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
        int x = int.Parse(Name.name);
        bool y = GameObject.FindGameObjectWithTag("hamakiller").GetComponent<Toggle>().isOn;
        PlayerNetwork.Instance.killsorc(x,y);
        StartCoroutine(hidePaper());




        //VoteManager. // awake // addvote // checkifvotecompleted // 
    }

    public void HelloFocus()
    {
        Debug.Log("HelloFocus !");
        GetComponent<soscript>().enabled = true;
    }

    public void ResetFocus()
    {
        MyTime = 0f;
        GetComponent<soscript>().enabled = false;
        ProgressLoader.fillAmount = MyTime / 3;


    }

    private IEnumerator hidePaper()
    {
        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("playercontso").GetComponent<Image>().color = new Color(255, 255, 255, 100);
        GameObject.FindGameObjectWithTag("papercanvasso").SetActive(false);
        

    }
}
