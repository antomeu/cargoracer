using UnityEngine;

public class ObjectController : MonoBehaviour {
    public ObjectType ObjectType;
    public GameObject ChildObject;
    
	void Start () {
		
	}

    void FixedUpdate() {
        if (Globals.GameState == GameState.Playing)
        {
            MoveObject();
            CullObject();
        }
    }

    private void MoveObject()
    {
        if (ObjectType == ObjectType.OncomingVehicle)
            transform.position += ( 5   + Globals.Speed) * Time.deltaTime * Vector3.back;
        else if (ObjectType == ObjectType.SameLaneVehicle)
            transform.position += (-5   + Globals.Speed) * Time.deltaTime * Vector3.back;
        else
            transform.position += Globals.Speed * Time.deltaTime * Vector3.back;
    }

    private void CullObject()
    {
        if (transform.position.z <= -60f)
        {
            transform.position += Globals.ClippingDistance * Vector3.forward;
            if (ObjectType == ObjectType.BonusBoost || ObjectType == ObjectType.Package || ObjectType == ObjectType.PackageDrop) //Reposition packages and drops randomly on a different lane
            {
                transform.position = new Vector3((Mathf.Floor(3.99f * Random.value)*6 - 9f), transform.position.y, transform.position.z); //random includes 0 and 1, so needs to be reduced a bit to exlude floor of 4 (which would be 4)
                if (ChildObject != null || !ChildObject.activeSelf)
                    ChildObject.SetActive(true);
            }
        }
        else if (transform.position.z >= Globals.ClippingDistance)
            transform.position -= Globals.ClippingDistance * Vector3.forward;
    }

    public void HideChildObject() // This hides the mesh, collider and rigidbodies but keep the script active
    {
        if (ChildObject != null)
            ChildObject.SetActive(false);
    }


}
