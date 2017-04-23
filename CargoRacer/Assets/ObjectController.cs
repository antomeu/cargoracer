using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.position += Globals.Speed * Time.deltaTime * Vector3.back;
        if (transform.position.z <= -20f || transform.position.z >= Globals.ClippingDistance)
        {
            Destroy(transform.gameObject);
        }
    }
}
