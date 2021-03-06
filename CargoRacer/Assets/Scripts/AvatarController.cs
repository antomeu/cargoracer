﻿using System;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    #region SET IN UNITY
    public float LaneWidth = 6f;
    public float LaneChangingSpeed = 36f;

    public float AccelerationRate = 1;
    public float DeccelerationRate = 1;

    public PackageSlotController[] PackageSlot = new PackageSlotController[3];

    public ParticleSystem ParticleCollision;
    public ParticleSystem ParticlePickUp;
    public ParticleSystem ParticleDropOff;
    public ParticleSystem ParticleBoost;
    public ParticleSystem ParticleTurnLeft;
    public ParticleSystem ParticleTurnRight;
    public ParticleSystem ParticleEndExplosion;
    public AudioManager AudioManager;

    

    public UIController UIController;

    public Animator CameraAnimator;

    public Animator AvatarAnimator;
    #endregion

    public float speed;
    private float[] LaneCoordinates = new float[4];
    private int currentLane = 2;
    private int movingDirection;
    private bool hasReachedLane;
    private bool isButtonLeftPressed;
    private bool isButtonRightPressed;
    

    void Start() {
        for (int i = 0; i <= 3; i++)
        {
            LaneCoordinates[i] = LaneWidth * (i - 3 / 2) - 3; //LaneWidth /2*((i - 2) + 1);
        }
        speed = 20f;
        currentLane = 2;
        transform.position = LaneCoordinates[currentLane] * Vector3.right;
    }

    void Update()
    {
        if (Globals.GameState == GameState.Playing)
        {
            MoveAvatarSideWays();
            isButtonLeftPressed = false;
            isButtonRightPressed = false;

            if (Globals.Lives > 0)
                ManageSpeed(Globals.NominalSpeed + Globals.SpeedIncrease[Globals.Level - 1]);
            else
                ManageSpeed(10);

            Globals.Speed = speed ;
            Globals.Distance += speed / Time.deltaTime;
            ManageAnimator();

        }
        else if (Globals.GameState == GameState.End)
        {
            ManageSpeed(10);
            Globals.Speed = speed;
        }

    }

    void ManageAnimator()
    {
        AvatarAnimator.SetFloat("Lives",Globals.Lives);
        AvatarAnimator.SetFloat("Speed",(Globals.Speed * 2)/(Globals.NominalSpeed + Globals.BoostAddition + Globals.SpeedIncrease[Globals.TotalLevels-1]));
    }

    public void ButtonLeftIsDown()
    {
        isButtonLeftPressed = true;
    }

    public void ButtonRightIsDown()
    {
        isButtonRightPressed = true;
    }

    void MoveAvatarSideWays()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || isButtonLeftPressed) && currentLane > 0)
        {
            movingDirection = -1;
            currentLane += movingDirection;
            ParticleTurnLeft.Play();
            AudioManager.TurnSkid.Play();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || isButtonRightPressed)&& currentLane < LaneCoordinates.Length - 1)
        {
            movingDirection = 1;
            currentLane += movingDirection;
            ParticleTurnRight.Play();
            AudioManager.TurnSkid.Play();
        }

        if (Math.Abs(transform.position.x - LaneCoordinates[currentLane]) > 0.1f)
        {
            var damping = Mathf.Abs(transform.position.x - LaneCoordinates[currentLane])/LaneWidth;
            transform.position += (Mathf.Sign(LaneCoordinates[currentLane] - transform.position.x ) * LaneChangingSpeed * Time.deltaTime) * damping * Vector3.right;
            transform.rotation = Quaternion.Euler(0,movingDirection * damping * 15f ,0);
        }
        else
        {
            hasReachedLane = true;
            transform.position = LaneCoordinates[currentLane] * Vector3.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    void ManageSpeed(float targetSpeed)
    {

        if (Mathf.Abs(speed - targetSpeed) > 0.1)
        {
            speed += (-speed + targetSpeed) * AccelerationRate * Time.deltaTime;
        }
        else
        {
            speed = targetSpeed;

        }
    }

    void OnTriggerEnter(Collider other)
    {
        var otherObjectType = other.gameObject.GetComponentInParent<ObjectController>().ObjectType;
        //Debug.Log(other.gameObject.name);
        if (otherObjectType == ObjectType.SameLaneVehicle)
        {
            speed = (Globals.NominalSpeed + Globals.SpeedIncrease[Globals.Level - 1]) - Globals.PenaltySpeed;
            CrashIntoTraffic();
        }
        else if (otherObjectType == ObjectType.OncomingVehicle)
        {
            speed = (Globals.NominalSpeed + Globals.SpeedIncrease[Globals.Level - 1]) - (Globals.PenaltySpeed +10f);
            CrashIntoTraffic();
        }
        else if (otherObjectType == ObjectType.BonusBoost)
        {
            speed = (Globals.NominalSpeed + Globals.SpeedIncrease[Globals.Level - 1]) + Globals.BoostAddition;
            ParticleBoost.Play();
            AudioManager.Boost.Play();
            CameraAnimator.SetTrigger("Boost");
        }
        else if (otherObjectType == ObjectType.Package)
        {
            PickPackageUp(other);
        }
        else if (otherObjectType == ObjectType.PackageDrop)
        {
            DropPackageOff(other);
        }

    }

    void CrashIntoTraffic()
    {
        Globals.Lives -= 1;
        ParticleCollision.Play();
        AudioManager.Crash.Play();
        CameraAnimator.SetTrigger("Shock");

        if (Globals.Lives <= 0)
        {
            AudioManager.FinalCrash.Play();
            ParticleEndExplosion.Play();
            CameraAnimator.SetTrigger("End");
        }
    }

    void PickPackageUp(Collider other)
    {
        //other.transform.parent.gameObject 
        for (int i = 0; i <= PackageSlot.Length-1; i++)
        {
            if (PackageSlot[i].SlotAvailable)
            {
                PackageSlot[i].ActivatePackage(PackageSlot[i], other.gameObject);
                Globals.PackageSlotIsUsed[i] = true;
                ParticlePickUp.transform.position = PackageSlot[i].transform.position;
                ParticlePickUp.Play();

                AudioManager.PickUp.Play();
                

                ObjectController otherObject = other.transform.parent.GetComponent<ObjectController>();
                otherObject.HideChildObject();

                return;
            }
        }

        if (!PackageSlot[PackageSlot.Length-1].SlotAvailable)//if full, do not pick up and show message
        {
            AudioManager.CannotPickUp.Play();
        }

    }
    

    void DropPackageOff(Collider other)
    {
        // Deactivate this slot with apropriate package
        for (int i = 0; i <= PackageSlot.Length-1; i++)
        {
            if (!PackageSlot[i].SlotAvailable)
            {
                PackageSlot[i].DeactivatePackages();
                Globals.PackageSlotIsUsed[i] = false;
                Globals.PackagesDelivered++;
                ParticleDropOff.transform.position = PackageSlot[i].transform.position;
                ParticleDropOff.Play();
                AudioManager.DropOff.Play();
                return;
            }
        }
    }


}
