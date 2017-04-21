using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    #region SET IN UNITY
    public float LaneWidth = 6f;
    public float LaneChangingSpeed = 6f;
    #endregion

    private float[] LaneCoordinates = new float[4];
    public int CurrentLane = 2;
    public int movingDirection;
    public bool hasReachedLane;

    // Use this for initialization
    void Start () {
        for (int i = 0; i <= 3; i++)
        {
            LaneCoordinates[i] = LaneWidth * (i - 3/2) - 3; //LaneWidth /2*((i - 2) + 1);
        }

        CurrentLane = 2;
        transform.position = LaneCoordinates[CurrentLane] * Vector3.right;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.LeftArrow) )
	    {
	        movingDirection = -1;
            CurrentLane += movingDirection;
        }

	    if (Input.GetKeyDown(KeyCode.RightArrow) )
	    {
	        movingDirection = 1;
            CurrentLane += movingDirection;
        }

	    //if (CurrentLane > 0 && CurrentLane < LaneCoordinates.Length && !hasReachedLane)
	    //{
            MoveAvatar();
        //}
    }

    void MoveAvatar()
    {
        if (Math.Abs(transform.position.x - LaneCoordinates[CurrentLane]) > 0.1f)
        {
            transform.position += ((movingDirection * LaneChangingSpeed) * Time.deltaTime) * Vector3.right;
            transform.rotation = Quaternion.Euler(0,movingDirection * 8f ,0);
            Debug.Log(LaneCoordinates[CurrentLane]);
        }
        else
        {
            hasReachedLane = true;
            transform.position = LaneCoordinates[CurrentLane] * Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
