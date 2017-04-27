using System;
using System.Net;
using UnityEngine;

public class AvatarController : MonoBehaviour
{

    #region SET IN UNITY
    public float LaneWidth = 6f;
    public float LaneChangingSpeed = 36f;

    public float AccelerationRate = 1;
    public float DeccelerationRate = 1;

    public PackageSlotController[] PackageSlot = new PackageSlotController[3];

    #endregion

    public float speed;
    private float[] LaneCoordinates = new float[4];
    private int currentLane = 2;
    private int movingDirection;
    private bool hasReachedLane;

    


    // Use this for initialization
    void Start () {
        for (int i = 0; i <= 3; i++)
        {
            LaneCoordinates[i] = LaneWidth * (i - 3/2) - 3; //LaneWidth /2*((i - 2) + 1);
        }
        speed = 20f;
        currentLane = 2;
        transform.position = LaneCoordinates[currentLane] * Vector3.right;
    }
	
	// Update is called once per frame
	void Update ()
	{
        MoveAvatarSideWays();
        ManageSpeed();
        Globals.Speed = speed;
	    Globals.Distance += speed/Time.deltaTime;

	}

    void MoveAvatarSideWays()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            movingDirection = -1;
            currentLane += movingDirection;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < LaneCoordinates.Length - 1)
        {
            movingDirection = 1;
            currentLane += movingDirection;
        }

        if (Math.Abs(transform.position.x - LaneCoordinates[currentLane]) > 0.1f)
        {
            var damping = Mathf.Abs(transform.position.x - LaneCoordinates[currentLane])/LaneWidth;
            transform.position += (movingDirection * LaneChangingSpeed * Time.deltaTime) * damping * Vector3.right;
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
            Globals.Lives--;
            
        }
        else if (otherObjectType == ObjectType.OncomingVehicle)
        {
            speed = 5f;
            Globals.Lives--;
        }
        else if (otherObjectType == ObjectType.BonusBoost)
            speed = 80f;
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
                ObjectController otherObject = other.GetComponent<ObjectController>();
                Debug.Log(otherObject);
                if (otherObject.ChildObject != null)// Disable package on the ground until respawn
                    otherObject.ChildObject.SetActive(false);
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
                return;
            }
        }
    }


}
