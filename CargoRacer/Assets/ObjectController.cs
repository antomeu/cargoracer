using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
    public ObjectType ObjectType;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        MoveObject();
        CullObject();
    }

    private void MoveObject()
    {
        if (ObjectType == ObjectType.OncomingVehicle)
            transform.position += ( 5 - 9/transform.position.x  + Globals.Speed) * Time.deltaTime * Vector3.back;
        else if (ObjectType == ObjectType.SameLaneVehicle)
            transform.position += (-5 - 9/transform.position.x  + Globals.Speed) * Time.deltaTime * Vector3.back;
        else
            transform.position += Globals.Speed * Time.deltaTime * Vector3.back;
    }

    void CullObject()
    {
        if (transform.position.z <= -20f)
            transform.position += Globals.ClippingDistance * Vector3.forward;
        else if (transform.position.z >= Globals.ClippingDistance)
            transform.position -= Globals.ClippingDistance * Vector3.forward;
    }

    void SendFlyinOff()
    {
        
    }

    void OnTriggerEnter1(Collider other)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
            rigidBody.useGravity = true;
            rigidBody.AddForce(20*Vector3.one);
        }

    }
}
