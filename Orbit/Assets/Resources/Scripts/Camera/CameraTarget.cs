using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour
{
	  // The position that that camera will be following.
	public GameObject target;
	public GameObject world;
	public float smoothing = 0f;        // The speed with which the camera will be following.
	public float minDist = 0.5f;

	public Vector3 offset;  // The initial offset from the target.
	
	public bool active = true;


	public bool fix = false;

	void Start() {
		setTarget (target);
	}

	void FixedUpdate ()
	{
		if (active) {
			if(!fix) {
				world.transform.position = -Vector3.Lerp (-world.transform.position, target.transform.position - world.transform.position, smoothing * Time.deltaTime) + offset;
				smoothing += 1f;

				if(smoothing >= 80) {
					fix = true;
				}

			} else {
				world.transform.position = -target.transform.position + world.transform.position;
			}
		}
	}

	public void setTarget(GameObject newTarget) {
		target = newTarget;
		fix = false;
		smoothing = 5f;
	}

}