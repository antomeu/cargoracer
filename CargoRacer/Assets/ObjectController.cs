using UnityEngine;

public class ObjectController : MonoBehaviour {
    public ObjectType ObjectType;
    public GameObject ChildObject;
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
        if (transform.position.z <= -60f)
        {
            transform.position += Globals.ClippingDistance * Vector3.forward;
            if (ObjectType == ObjectType.BonusBoost || ObjectType == ObjectType.Package || ObjectType == ObjectType.PackageDrop)
            {
                transform.position = new Vector3((Mathf.Ceil(18 * Random.value) - 9f), transform.position.y, transform.position.z);
                if (ChildObject != null || ChildObject.activeSelf)
                    ChildObject.SetActive(true);
            }
        }
        else if (transform.position.z >= Globals.ClippingDistance)
            transform.position -= Globals.ClippingDistance * Vector3.forward;
    }




}
