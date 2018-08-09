using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHero : MonoBehaviour
{

    private float speed = 0.5f;
    // public float speedd = 1f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Vector3 dragOrigin;
    void Update()
    {
        transform.Rotate(new Vector3(0, speed, 0));
        // Swipe();
    }
    /* public void Swipe()
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
                     float rotY = transform.eulerAngles.y * speedd * Time.deltaTime;                 
                     transform.Rotate(Vector3.up, rotY);
                     Debug.Log("left swipe");
                 }
                 //swipe right
                 if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
              {
                     float rotY = transform.eulerAngles.y * speedd * Time.deltaTime;
                     transform.Rotate(Vector3.down, rotY);
                     Debug.Log("right swipe");
                 }
             }
         }
     }*/

}
