using UnityEngine;

public class CameraControl : MonoBehaviour {
	
	bool leftClickFlag = true;

    RaycastHit hit;

    Actor actorScript;
	
	public GameObject actor;
	public string nodeTag;
	
	void Start()
	{
		if (actor != null)
		{
			actorScript = (Actor)actor.GetComponent(typeof(Actor));
		}
	}
	
	void Update () 
	{
		/***Left Click****/
		if (Input.GetKey(KeyCode.Mouse0) && leftClickFlag)
			leftClickFlag = false;
		
        //Fires ray the where mouse clicked, if the tag of hit object
        //is the same as nodeTag and that object is not visited, move player
        //to that object
		if (!Input.GetKey(KeyCode.Mouse0) && !leftClickFlag)
		{
			leftClickFlag = true;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.transform.tag == nodeTag)
				{
                    Debug.Log("Hit");
					
                    if(!hit.transform.GetComponent<NodeProperties>().getVisited()){
                        actorScript.MoveOrder(hit.transform.gameObject);
                    }
				}
			}
		}
	}
}
