using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    #region SET IN UNITY
    public float LaneWidth = 6f;
    public float LaneChangingSpeed = 36f;
    #endregion

    public float speed;
    private float[] LaneCoordinates = new float[4];
    public int CurrentLane = 2;
    private int movingDirection;
    private bool hasReachedLane;



    // Use this for initialization
    void Start () {
        for (int i = 0; i <= 3; i++)
        {
            LaneCoordinates[i] = LaneWidth * (i - 3/2) - 3; //LaneWidth /2*((i - 2) + 1);
        }
        speed = 20f;
        CurrentLane = 2;
        transform.position = LaneCoordinates[CurrentLane] * Vector3.right;
    }
	
	// Update is called once per frame
	void Update ()
	{
        MoveAvatar();
        Globals.Speed = speed;

    }

    void MoveAvatar()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CurrentLane > 0)
        {
            movingDirection = -1;
            CurrentLane += movingDirection;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && CurrentLane < LaneCoordinates.Length - 1)
        {
            movingDirection = 1;
            CurrentLane += movingDirection;
        }

        if (Math.Abs(transform.position.x - LaneCoordinates[CurrentLane]) > 0.1f)
        {
            var damping = Mathf.Abs(transform.position.x - LaneCoordinates[CurrentLane])/LaneWidth;
            transform.position += (movingDirection * LaneChangingSpeed * Time.deltaTime) * damping * Vector3.right;
            transform.rotation = Quaternion.Euler(0,movingDirection * damping * 15f ,0);
        }
        else
        {
            hasReachedLane = true;
            transform.position = LaneCoordinates[CurrentLane] * Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
