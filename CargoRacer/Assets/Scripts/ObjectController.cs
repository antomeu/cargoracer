using UnityEngine;

public class ObjectController : MonoBehaviour {
    public ObjectType ObjectType;
    public GameObject ChildObject;
    public DecorationLevelController DecorationLevelController;
    
	void Start () {
		
	}

    void FixedUpdate() {
        if (Globals.GameState == GameState.Playing || Globals.GameState == GameState.End)
        {
            MoveObject();
            CullObject();
        }
    }

    private void MoveObject()
    {
        if (ObjectType == ObjectType.OncomingVehicle)
            transform.position += ( Globals.TrafficSpeed[Globals.Level-1]   + Globals.Speed) * Time.deltaTime * Vector3.back;
        else if (ObjectType == ObjectType.SameLaneVehicle)
            transform.position += (-Globals.TrafficSpeed[Globals.Level-1]   + Globals.Speed) * Time.deltaTime * Vector3.back;
        else
            transform.position += Globals.Speed * Time.deltaTime * Vector3.back;
    }

    private void CullObject()
    {
        if (transform.position.z <= -60f)
        {
            transform.position += Globals.ClippingDistance * Vector3.forward;

            if ((ObjectType == ObjectType.House || ObjectType == ObjectType.Tree ) && DecorationLevelController != null)
            {
                DecorationLevelController.SwitchDecorationForLevelChange(Globals.Level);
            }

            if (ObjectType == ObjectType.BonusBoost || ObjectType == ObjectType.Package || ObjectType == ObjectType.PackageDrop)
            { //Reposition packages and drops randomly on a different lane. //random includes 0 and 1, so needs to be reduced a bit to exlude floor of 4 (which would be 4)
                transform.position = new Vector3((Mathf.Floor(3.99f * Random.value)*6 - 9f), transform.position.y, transform.position.z); 
                if (ChildObject != null || !ChildObject.activeSelf)
                    ChildObject.SetActive(true);
            }
        }
        else if (transform.position.z >= Globals.ClippingDistance)
            transform.position -= Globals.ClippingDistance * Vector3.forward;
    }

    public void HideChildObject() // This hides the mesh, collider and rigidbodies but keep the script active. Used to hide packages after pick up
    {
        if (ChildObject != null)
            ChildObject.SetActive(false);
    }


}
