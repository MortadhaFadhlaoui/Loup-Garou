using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testforscene : MonoBehaviour {
    public Text txt;
    public Text cam;
    public Button btn;
    public Text secondtxt;
    public Image img;
    string story;
    // Use this for initialization
    void Start () {
        PlayerPrefs.DeleteAll();
        cam.gameObject.SetActive(false);
        btn.gameObject.SetActive(false);
        img.gameObject.SetActive(false);
        secondtxt.gameObject.SetActive(false);
        int test = PlayerPrefs.GetInt("test");
        if(test == 0)
        {
            cam.gameObject.SetActive(true);
            btn.gameObject.SetActive(true);
            img.gameObject.SetActive(true);
            secondtxt.gameObject.SetActive(true);
            StartCoroutine("PlayText");
            
        }
        else
        {
            Application.LoadLevel("Home");
            Destroy(this);
        }
	}


    IEnumerator PlayText()
    {
        story = "Game Introduction will start in 10.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 9.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 8.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 7.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 6.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 5.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 4.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 3.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 2.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 1.";
        yield return new WaitForSeconds(1f);
        txt.text = "Game Introduction will start in 0.";
        Application.LoadLevel("ScenematicIntro");
        Destroy(this);

    }

    // Update is called once per frame
    void Update () {
		
	}
    
    public void Skip()
    {
        Debug.Log("ok");
        PlayerPrefs.SetInt("test", 1);
        Application.LoadLevel("Home");
        Destroy(this);
    }
}
