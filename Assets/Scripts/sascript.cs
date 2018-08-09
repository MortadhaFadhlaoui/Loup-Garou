using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sascript : MonoBehaviour
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
        PlayerNetwork.Instance.setSetProtectedBySalavator(int.Parse( Name.name));
        StartCoroutine(hidePaper());




        //VoteManager. // awake // addvote // checkifvotecompleted // 
    }

    public void HelloFocus()
    {
        Debug.Log("HelloFocus !");
        GetComponent<sascript>().enabled = true;
    }

    public void ResetFocus()
    {
        MyTime = 0f;
        GetComponent<sascript>().enabled = false;
        ProgressLoader.fillAmount = MyTime / 3;


    }

    private IEnumerator hidePaper()
    {
        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("playercontsa").GetComponent<Image>().color = new Color(255, 255, 255, 100);
        GameObject.FindGameObjectWithTag("papercanvassa").SetActive(false);
        

    }
}
