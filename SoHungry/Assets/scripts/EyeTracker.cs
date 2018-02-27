using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracker : MonoBehaviour {

    [Header("Bounding Box Objects")]
    [Tooltip("Bounding box objects used to limit movement")]
    public GameObject[] boundingObject; //Object to set bounds of followObject
    [Header("Follow Objects")]
    [Tooltip("Objects that follow mouse movement")]
    public GameObject[] followObject; //Objects to follow cursor
    
    //sets up min/max local position values     
    private float[] minX;
    private float[] maxX;
    private float[] minY;
    private float[] maxY;

    private Vector3[] startingPosition; //Array to hold starting position

    void Start ()
    {
        SetStartingPosition();
        SetBoundingBox();
    }

    void Update () {
        if (itemSelected.itemSelectedBool)
        {
            FollowCursor();
        }
        else
        {
            ResetPosition();
        }

	}

    //Boolean to check if object should follow
    public static class itemSelected
    {
       public static bool itemSelectedBool;
    }

    //Grabs initial starting position to get a reset value
    void SetStartingPosition()
    {
        startingPosition = new Vector3[followObject.Length];

        for (int i = 0; i < followObject.Length; i++)
        {
            Vector3 followObjectLocal;
            followObjectLocal = followObject[i].transform.localPosition;
            startingPosition[i] = followObjectLocal;
        }
    }

    //Resets positions to starting position
     void ResetPosition()
    {
        for (int i = 0; i < followObject.Length; i++)
        {
            followObject[i].transform.localPosition = startingPosition[i];
        }
    }

    //Sets bounding box based on boundingObject sprites
    void SetBoundingBox()
    {
        minX = new float[boundingObject.Length];
        maxX = new float[boundingObject.Length];
        minY = new float[boundingObject.Length];
        maxY = new float[boundingObject.Length];

        for (int i = 0; i < boundingObject.Length; i++)
        {
            //Grab bounding object size
            float boundingObjectWidth = boundingObject[i].GetComponent<SpriteRenderer>().bounds.size.x;
            float boundingObjectHeight = boundingObject[i].GetComponent<SpriteRenderer>().bounds.size.y;

            //grab follow object size
            float followObjectWidth = followObject[i].GetComponent<SpriteRenderer>().bounds.size.x;
            float followObjectHeight = followObject[i].GetComponent<SpriteRenderer>().bounds.size.y;

            //min = (bounding object size/2 - follow object size/2) * -1
            //max = bounding object size/2 - follow object size/2
            minX[i] = (boundingObjectWidth / 2 - followObjectWidth/2) * -1f;
            maxX[i] = boundingObjectWidth / 2 - followObjectWidth / 2;
            minY[i] = (boundingObjectHeight / 2 - followObjectHeight / 2) * -1f;
            maxY[i] = boundingObjectHeight / 2 - followObjectHeight / 2;
        }

    }

    void FollowCursor()
    {
        for (int i = 0; i < followObject.Length; i++)
        {
            Vector3 followObjectPosition = followObject[i].transform.position; //grabs world position of object
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //grabs world position of cursor

            Vector3 cursorDirection = cursorPosition - followObjectPosition; //gets the difference of the two positions to determine direction

            //Sets local position based on direction and min/max positions.
            Vector3 pos = followObject[i].transform.localPosition;
            pos.x = Mathf.Clamp(pos.x + cursorDirection.x, minX[i], maxX[i]);
            pos.y = Mathf.Clamp(pos.y + cursorDirection.y, minY[i], maxY[i]);

            followObject[i].transform.localPosition = pos;

        }
    }
}
