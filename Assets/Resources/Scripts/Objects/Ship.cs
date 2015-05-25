using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour, IRecycle {

	public Vector2 startingPosition;
	/*public GameObject camera;
	public GameObject earth;*/

	// Use this for initialization

	void Start() {
		//gameObject.GetComponent<Renderer>().material.mainTexture.mipMapBias = 3;
	}

	void FixedUpdate() {
		//transform.position = (transform.position + new Vector3 (1,0,0));

		/*if (camera.GetComponent<CameraTarget> ().target.name.Equals (this.name) && camera.GetComponent<CameraTarget> ().fix) {
			camera.GetComponent<CameraTarget> ().setTarget (earth);
		}*/

		if(transform.position.x > 100)
			ElementUtil.Destroy(gameObject);
	}

	public void Restart(){
		print("Ship restart");
		GetComponent<Rigidbody2D>().velocity = new Vector3 (100,0,0);
	}

	public void Shutdown() {
		print ("Ship Shutdown");
	}

	public void Startup() {
		print ("Ship Startup");
		Restart();
	
	}
}
