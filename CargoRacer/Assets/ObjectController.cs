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
    void FixedUpdate() {
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
        if (transform.position.z <= -40f)
            transform.position += Globals.ClippingDistance * Vector3.forward;
        else if (transform.position.z >= Globals.ClippingDistance)
            transform.position -= Globals.ClippingDistance * Vector3.forward;
    }




}
