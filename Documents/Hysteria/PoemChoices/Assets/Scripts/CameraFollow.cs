using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform lookAt;

	private Vector3 positionOffSet = new Vector3(0, 0, -2.0f);

	private void Start () {

        transform.position = lookAt.transform.position + positionOffSet;
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}
