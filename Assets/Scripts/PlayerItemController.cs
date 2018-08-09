using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemController : MonoBehaviour {

    public Text Name;
    public Toggle Checked;
    public float MyTime = 0f;
    private Image ProgressLoader;

	// Use this for initialization
	void Start () {
        ProgressLoader = GameObject.FindGameObjectWithTag("CirclePointerImage").GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        MyTime += Time.deltaTime;
        ProgressLoader.fillAmount = MyTime / 3;

        if (MyTime >= 3f)
        {
            OnFocusItem();
        }

    }

    public void OnFocusItem()
    {/*
        if(VoteManager.instance.isVoted == false)
        {
            Checked.isOn = true;
            VoteManager.instance.AddVote(Name.text);
        }
        */
        //VoteManager. // awake // addvote // checkifvotecompleted // 
        Debug.Log("ok");
    }

    public void HelloFocus()
    {
        Debug.Log("HelloFocus !");
        GetComponent<PlayerItemController>().enabled = true;
    }

    public void ResetFocus()
    {
        MyTime = 0f;
        GetComponent<PlayerItemController>().enabled = false;
        ProgressLoader.fillAmount = MyTime / 3;


    }
}
