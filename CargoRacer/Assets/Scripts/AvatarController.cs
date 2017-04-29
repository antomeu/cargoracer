using System;
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
            ManageSpeed();
            Globals.Speed = speed;
            Globals.Distance += speed / Time.deltaTime;
        }

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
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || isButtonRightPressed)&& currentLane < LaneCoordinates.Length - 1)
        {
            movingDirection = 1;
            currentLane += movingDirection;
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

    void ManageSpeed()
    {
        if (Mathf.Abs(speed - Globals.NominalSpeed) > 0.1)
        {
            speed += (-speed + Globals.NominalSpeed) * AccelerationRate * Time.deltaTime;
        }
        else
        {
            speed = Globals.NominalSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var otherObjectType = other.gameObject.GetComponentInParent<ObjectController>().ObjectType;
        //Debug.Log(other.gameObject.name);
        if (otherObjectType == ObjectType.SameLaneVehicle)
        {
            speed = 15f;
            Globals.Lives -= 1;
            ParticleCollision.Play();
        }
        else if (otherObjectType == ObjectType.OncomingVehicle)
        {
            speed = 5f;
            Globals.Lives -= 1;
            ParticleCollision.Play();
        }
        else if (otherObjectType == ObjectType.BonusBoost)
        {
            speed = 80f;
            ParticleBoost.Play();
        }
        else if (otherObjectType == ObjectType.Package)
            PickPackageUp(other);
        else if (otherObjectType == ObjectType.PackageDrop)
            DropPackageOff(other);

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

                ObjectController otherObject = other.transform.parent.GetComponent<ObjectController>();
                otherObject.HideChildObject();

                return;
            }
        }

        if (!PackageSlot[PackageSlot.Length-1].SlotAvailable)//if full, do not pick up and show message
        {
            return;
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
                return;
            }
        }
    }


}
