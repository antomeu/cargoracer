using UnityEngine;

public class VehicleCollisionController : MonoBehaviour
{
    public Rigidbody VehicleRigidbody;
    public Collider VehiclCollider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= -10)
        {
            VehicleRigidbody.isKinematic = true;
            VehicleRigidbody.useGravity = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            VehiclCollider.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        
        if (VehicleRigidbody != null)
        {
            //Debug.Log(other.gameObject);
            if (other.name == "Player")
            {
                VehicleRigidbody.isKinematic = false;
                VehicleRigidbody.useGravity = true;
                if (GetComponentInParent<ObjectController>().ObjectType == ObjectType.OncomingVehicle)
                {
                    VehicleRigidbody.AddForce(600* new Vector3(-1,1,1));
                    VehicleRigidbody.AddTorque(0,0,500 );
                }
                if (GetComponentInParent<ObjectController>().ObjectType == ObjectType.SameLaneVehicle)
                {
                    VehicleRigidbody.AddForce(600 * new Vector3(1, 1, 1));
                    VehicleRigidbody.AddTorque(0, 0, -500);
                }
                VehiclCollider.enabled = false;
            }
        }
    }
}
