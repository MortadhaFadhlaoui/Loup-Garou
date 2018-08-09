using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howto : MonoBehaviour {
    public GameObject first,first1,second,third,forth,fifth,last;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void skip1()
    {
        first1.SetActive(false);
        first.SetActive(false);
    }
    public void showfirst()
    {
        first.SetActive(true);
        first1.SetActive(true);
    }

    public void skip2()
    {
        second.SetActive(false);
        first.SetActive(false);
    }
    public void showsecond()
    {
        second.SetActive(true);
        first1.SetActive(false);
    }

    public void skip3()
    {
        third.SetActive(false);
        first.SetActive(false);
    }
    public void showthird()
    {
        third.SetActive(true);
        second.SetActive(false);
    }

    public void skip4()
    {
        forth.SetActive(false);
        first.SetActive(false);
    }
    public void showforth()
    {
        third.SetActive(false);
        forth.SetActive(true);
    }

    public void skip5()
    {
        fifth.SetActive(false);
        first.SetActive(false);
    }
    public void showfifth()
    {
        forth.SetActive(false);
        fifth.SetActive(true);
    }
    public void skip6()
    {
        last.SetActive(false);
        first.SetActive(false);
        first1.SetActive(false);
    }
    public void showlast()
    {
        fifth.SetActive(false);
        last.SetActive(true);
    }

}
