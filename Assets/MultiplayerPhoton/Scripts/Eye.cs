using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {
    public GameObject EyeClose, EyeOpen,Backgound,PointLight,Fire,Sun;
    private bool EyeCloseIn =  true;
    private float time = 45f;
    private GameObject player;
    // Use this for initialization
    void Start () {
        Debug.Log(PhotonNetwork.player.TagObject+"zaaaaaaaaaa");
        OnEyeClose();

    }

    // Update is called once per frame
    void Update () {
        
        
       /* if (time <= 38 && time <= 23)
        {
            EyeClose.GetComponent<AudioSource>().mute = true;
        }
        if (time <= 23 && time <= 16)
        {
            EyeClose.GetComponent<AudioSource>().mute = false;
        }
        if (time <= 16 && time <= 0)
        {
            EyeClose.GetComponent<AudioSource>().mute = true;
        }*/
    }
    public void closeme()
    {
        OnEyeOpen();
        EyeCloseIn = false;
    }
  
    public void OnEyeClose()
    {
        
        Fire.gameObject.SetActive(false);
        Sun.gameObject.SetActive(false);       
        EyeClose.gameObject.SetActive(true);       
        StartCoroutine(SetAnimationAfterCloseEyes());
    }
    public void OnEyeOpen()
    {       
        //PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);       
        EyeClose.gameObject.SetActive(false);
        Backgound.gameObject.SetActive(false);
        PointLight.gameObject.SetActive(false);
        EyeOpen.gameObject.SetActive(true);
        Fire.gameObject.SetActive(true);
        Sun.gameObject.SetActive(true);
       // StartCoroutine(Die());
    }
  
    private IEnumerator SetAnimationAfterCloseEyes()
    {
        yield return new WaitForSeconds(3f);
        Backgound.gameObject.SetActive(true);
        PointLight.gameObject.SetActive(true);
    }
   /* private IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        player = GameObject.FindGameObjectWithTag("Player"+PhotonNetwork.player.ID);
        player.GetComponent<Animation>().Play("die");
    }*/
}
