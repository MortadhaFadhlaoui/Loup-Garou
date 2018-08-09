using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killsc : MonoBehaviour
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

        if (MyTime >= 2f)
        {
            Debug.Log("feeeeet");
            OnFocusItem();
        }

    }

    public void OnFocusItem()
    {
        
        Debug.Log("done");
        Checked.isOn = !Checked.isOn;



        //VoteManager. // awake // addvote // checkifvotecompleted // 
    }

    public void HelloFocus()
    {
        Debug.Log("HelloFocus !");
        GetComponent<killsc>().enabled = true;
    }

    public void ResetFocus()
    {
        MyTime = 0f;
        GetComponent<killsc>().enabled = false;
        ProgressLoader.fillAmount = MyTime / 3;


    }

  
}
