using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    public GameObject look,spot1,spo2;
    public Button Done;
    public GameObject male_04, female_03, female_01, male_03, male_06;
    public GameObject ChampionSelect;
    public GameObject LobbyPanel;
    public GameObject DefaultChampionList;
    public GameObject RightChampionList;
    public GameObject LeftChampionList;
    public GameObject ChampionList4;
    public GameObject ChampionList5;
    public GameObject CenterPoint;
    public GameObject LeftPoint;
    public GameObject RightPoint;
    public GameObject BackGroundPoint;
    public float speedd = 1f;
    public static int mainchamp = 1;
    Vector2 firstPressPos;
    private bool testdone= true;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    GameObject x = null;
    Vector3 dragOrigin;
    int i = 0;
    private List<GameObject> ChampionsList;
    // Use this for initialization
    void Start()
    {
        ChampionsList = new List<GameObject>();
        ChampionsList.Add(LeftChampionList);
        ChampionsList.Add(DefaultChampionList);
        ChampionsList.Add(RightChampionList);
        ChampionsList.Add(ChampionList4);
        ChampionsList.Add(ChampionList5);

        Debug.Log(ChampionsList.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
    }
    public void OnCenterChampion(GameObject o)
    {
        o.transform.localScale = new Vector3(190, 190, 190);
        o.transform.position = new Vector3(CenterPoint.transform.position.x, CenterPoint.transform.position.y, CenterPoint.transform.position.z);
    }

    public void OnLeftChampion(GameObject o)
    {
        o.transform.localScale = new Vector3(150, 150, 150);
        o.transform.position = new Vector3(LeftPoint.transform.position.x, LeftPoint.transform.position.y, LeftPoint.transform.position.z);
    }

    public void OnRightChampion(GameObject o)
    {
        o.transform.localScale = new Vector3(150, 150, 150);
        o.transform.position = new Vector3(RightPoint.transform.position.x, RightPoint.transform.position.y, RightPoint.transform.position.z);
    }

    public void OnBackChampion(GameObject o)
    {
        o.transform.position = new Vector3(BackGroundPoint.transform.position.x, BackGroundPoint.transform.position.y, BackGroundPoint.transform.position.z);
    }

    public void OnDone()
    {
        testdone = true;
        if(mainchamp == 3)
        {
            int x = PlayerPrefs.GetInt("c1");
            if(x == 0)
            {
                testdone = false;
            }
        }
        if (mainchamp == 4)
        {
            int xx = PlayerPrefs.GetInt("c2");
            if (xx == 0)
            {
                testdone = false;
            }
        }


        if (testdone)
        {

      
        Done.gameObject.SetActive(false);
        LobbyPanel.gameObject.SetActive(true);
        ChampionSelect.gameObject.SetActive(false);

        switch (mainchamp)
        {
            case 1:
                male_04.SetActive(true);
                female_03.SetActive(false);
                female_01.SetActive(false);
                male_03.SetActive(false);
                male_06.SetActive(false);
                break;
            case 0:
                male_04.SetActive(false);
                female_03.SetActive(false);
                female_01.SetActive(false);
                male_03.SetActive(true);
                male_06.SetActive(false);
                break;
            case 2:
                male_04.SetActive(false);
                female_03.SetActive(true);
                female_01.SetActive(false);
                male_03.SetActive(false);
                male_06.SetActive(false);
                break;
            case 3:
                male_04.SetActive(false);
                female_03.SetActive(false);
                female_01.SetActive(false);
                male_03.SetActive(false);
                male_06.SetActive(true);
                break;
            case 4:
                male_04.SetActive(false);
                female_03.SetActive(false);
                female_01.SetActive(true);
                male_03.SetActive(false);
                male_06.SetActive(false);
                break;
            }
        }
        else
        {
            AndroidNativeFunctions.ShowToast("You don't own this skin");
        }
    }

    public void OnAvatarPicker()
    {   
        Done.gameObject.SetActive(true);
        LobbyPanel.gameObject.SetActive(false);
        ChampionSelect.gameObject.SetActive(true);
    }

    public void Swipe()
    {

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                dragOrigin = Input.mousePosition;

                Debug.Log("time :  " + dragOrigin);
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    spot1.SetActive(true);
                    spo2.SetActive(false);
                    look.SetActive(false);
                
                    if (mainchamp < 4)
                    {


                        mainchamp = mainchamp + 1;
                    }
                    if(mainchamp == 3)
                    {
                        int x = PlayerPrefs.GetInt("c1");
                        if (x == 0)
                        {
                            spot1.SetActive(false);
                            spo2.SetActive(true);
                            look.SetActive(true);
                        }
                    }
                    if (mainchamp == 4)
                    {
                        int xx = PlayerPrefs.GetInt("c2");
                        if (xx == 0)
                        {
                            spot1.SetActive(false);
                            spo2.SetActive(true);
                            look.SetActive(true);
                        }
                    }

                    if (mainchamp == 4)
                    {
                        ChampionsList[3].GetComponent<AudioSource>().Stop();
                        ChampionsList[mainchamp].GetComponent<AudioSource>().Play();
                        ChampionsList[3].GetComponent<Animation>().Stop();
                        ChampionsList[mainchamp].GetComponent<Animation>().Play();
                        OnCenterChampion(ChampionsList[mainchamp]);
                        OnLeftChampion(ChampionsList[3]);
                        OnBackChampion(ChampionsList[2]);

                    }
                    else if (mainchamp == 1)
                    {
                        ChampionsList[mainchamp].GetComponent<AudioSource>().Play();
                        ChampionsList[mainchamp - 1].GetComponent<AudioSource>().Stop();
                        ChampionsList[mainchamp].GetComponent<Animation>().Play();
                        ChampionsList[mainchamp - 1].GetComponent<Animation>().Stop();
                        OnRightChampion(ChampionsList[mainchamp + 1]);
                        OnCenterChampion(ChampionsList[mainchamp]);
                        OnLeftChampion(ChampionsList[mainchamp - 1]);
                    }
                    else
                    {
                        ChampionsList[mainchamp].GetComponent<AudioSource>().Play();
                        ChampionsList[mainchamp - 1].GetComponent<AudioSource>().Stop();
                        ChampionsList[mainchamp].GetComponent<Animation>().Play();
                        ChampionsList[mainchamp - 1].GetComponent<Animation>().Stop();
                        OnRightChampion(ChampionsList[mainchamp + 1]);
                        OnCenterChampion(ChampionsList[mainchamp]);
                        OnLeftChampion(ChampionsList[mainchamp - 1]);
                        OnBackChampion(ChampionsList[mainchamp - 2]);
                    }

                    Debug.Log(ChampionsList.ToString());
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {

                    spot1.SetActive(true);
                    spo2.SetActive(false);
                    look.SetActive(false);


                    if (mainchamp > 0)
                    {


                        mainchamp = mainchamp - 1;
                    }


                    if (mainchamp == 3)
                    {
                        int x = PlayerPrefs.GetInt("c1");
                        if (x == 0)
                        {
                            spot1.SetActive(false);
                            spo2.SetActive(true);
                            look.SetActive(true);
                        }
                    }
                    if (mainchamp == 4)
                    {
                        int xx = PlayerPrefs.GetInt("c2");
                        if (xx == 0)
                        {
                            spot1.SetActive(false);
                            spo2.SetActive(true);
                            look.SetActive(true);
                        }
                    }

                   
                    if (mainchamp == 0)
                    {
                        ChampionsList[1].GetComponent<AudioSource>().Stop();
                        ChampionsList[mainchamp].GetComponent<AudioSource>().Play();
                        ChampionsList[1].GetComponent<Animation>().Stop();
                        ChampionsList[mainchamp].GetComponent<Animation>().Play();
                        OnCenterChampion(ChampionsList[mainchamp]);
                        OnRightChampion(ChampionsList[1]);
                        OnBackChampion(ChampionsList[2]);

                    }
                    else if (mainchamp == 3)
                    {
                        ChampionsList[mainchamp + 1].GetComponent<AudioSource>().Stop();
                        ChampionsList[mainchamp].GetComponent<AudioSource>().Play();
                        ChampionsList[mainchamp + 1].GetComponent<Animation>().Stop();
                        ChampionsList[mainchamp].GetComponent<Animation>().Play();
                        OnRightChampion(ChampionsList[mainchamp + 1]);
                        OnCenterChampion(ChampionsList[mainchamp]);
                        OnLeftChampion(ChampionsList[mainchamp - 1]);

                    }
                    else
                    {
                        ChampionsList[mainchamp + 1].GetComponent<AudioSource>().Stop();
                        ChampionsList[mainchamp].GetComponent<AudioSource>().Play();
                        ChampionsList[mainchamp + 1].GetComponent<Animation>().Stop();
                        ChampionsList[mainchamp].GetComponent<Animation>().Play();
                        OnRightChampion(ChampionsList[mainchamp + 1]);
                        OnCenterChampion(ChampionsList[mainchamp]);
                        OnLeftChampion(ChampionsList[mainchamp - 1]);
                        OnBackChampion(ChampionsList[mainchamp + 2]);
                    }

                    Debug.Log(ChampionsList.ToString());
                    Debug.Log("left swipe");


                }
            }
        }

    }
}