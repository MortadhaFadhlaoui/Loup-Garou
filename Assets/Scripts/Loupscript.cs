using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loupscript : MonoBehaviour
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
        if(VoteManager.instance.isVoted == false)
        {
            Debug.Log("i've foted");
            VoteManager.instance.AddVote(Name.name);
         
            
        }
      
       



        //VoteManager. // awake // addvote // checkifvotecompleted // 
    }

    public void HelloFocus()
    {
        Debug.Log("HelloFocus !");
        GetComponent<Loupscript>().enabled = true;
    }

    public void ResetFocus()
    {
        MyTime = 0f;
        GetComponent<Loupscript>().enabled = false;
        ProgressLoader.fillAmount = MyTime / 3;


    }

   
}
