using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyScript : Photon.MonoBehaviour {

    [SerializeField]
    private PhotonVoiceRecorder recorder;
    /*public Color color = Color.black;
    public Color color1 = Color.blue;*/


    // Use this for initialization
    void Start () {
        GetComponent<PhotonVoiceRecorder>().DebugEchoMode = false;
        
        if (photonView.isMine)
        {
            GetComponent<PhotonVoiceRecorder>().enabled = true;
        }
        /*color.g = 0f;
        color.r = 0f;
        color.b = 0f;
        color.a = 0f;

        color1.g = 10f;
        color1.r = 10f;
        color1.b = 10f;
        color1.a = 10f;*/
    }
	
	// Update is called once per frame
	void Update () {
       
        if (photonView.isMine )
        {
          /*
            float speed = 1f;
            float height = 0.9f;
            Vector3 pos = transform.position;
            float newY = Mathf.Sin(Time.time * speed);
            transform.position = new Vector3(pos.x, newY + 1, pos.z) * height;
            */
            if (recorder.LevelMeter.CurrentPeakAmp > 2500)
            {
                GetComponent<Renderer>().material.color = Color.black;
                Debug.Log("ya7ki");
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.magenta;

            }

        }
       
      

        
	}
    
    
    
}
