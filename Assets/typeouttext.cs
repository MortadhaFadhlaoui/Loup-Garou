using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class typeouttext : MonoBehaviour 
{
    int vrModeInt;

    public Text txt;
     string story;
    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = enable;
    }

    void EnableVR()
    {
        StartCoroutine(LoadDevice("cardboard", true));
    }

    void DisableVR()
    {
        StartCoroutine(LoadDevice("", false));
    }
    void Awake()
{
        
        
    txt = GetComponent<Text>();
    
    txt.text = "";

    // TODO: add optional delay when to start
    
}
    void OnTriggerEnter(Collider col)
    {

        if (col.CompareTag("cube"))
        {

            StartCoroutine("PlayText");
        }
        if (col.CompareTag("cube1"))
        {

            StartCoroutine("PlayText1");
        }
        if (col.CompareTag("cube2"))
        {

            StartCoroutine("PlayText2");
        }

    }
    IEnumerator PlayText()
{
        
        story = "              WereWolf: \nThe Village is your mission ";
    foreach (char c in story)
    {
        txt.text += c;
        yield return new WaitForSeconds(0.04f);
    }
        txt.text = "";
        yield return new WaitForSeconds(0.5f);
        
        story = "You will attempt to save it or \n              destroy it.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.01f);
        }

        
        yield return new WaitForSeconds(0.5f);
        txt.text = "";
        story = "One minute you're a Villager, \ndefending your theoretical home with every fiber of your being.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        
        yield return new WaitForSeconds(1f);
        txt.text = "";
        story = "The next, you're a Werewolf, framing your friends and accusing them of wanting to destroy it.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        txt.text = "";
        story = "the game is designed to test your personal judgement and moral character.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        txt.text = "";
       
        
    }
    IEnumerator PlayText1()
    {


        story = "there are many cards in the game, wich are distributed automatically.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
        }


        yield return new WaitForSeconds(1f);
        txt.text = "";
        story = "there is the werewolfs that will try to take over the village.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        txt.text = "";
        story = "the normal werewolf and the alpha werwolf.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        txt.text = "";
        story = "there is also the villagers, that will try to defend it.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(1f);
        txt.text = "";
        story = "the normal villager, the doctor, the witch, the moderator and the drunk.";
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.04f);
        }
        yield return new WaitForSeconds(2f);
        txt.text = "";

    }

    IEnumerator PlayText2()
    {

        yield return new WaitForSeconds(3f);
       
        PlayerPrefs.SetInt("test", 1);

        DisableVR();

        Application.LoadLevel("Home");
        Destroy(this);
    }
}