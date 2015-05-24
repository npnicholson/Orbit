using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementPool : MonoBehaviour {
	
	public RecycleElement prefab;
	
	private List<RecycleElement> poolInstances = new List<RecycleElement>();

	/**
	 * Create a new instance, add it to this pool, and place it at the given location
	 * @param pos a Vector3 position to place the new RecycleElement at
	 * @return the RecycleElement that was created
	 */
	private RecycleElement CreateInstance(Vector3 pos) {

		// Instantiate a new prefab and place it at the correct location
		var clone = GameObject.Instantiate (prefab);
		clone.transform.position = pos;
		clone.transform.parent = transform;

		// Add the new prefab to the pool
		poolInstances.Add (clone);

		// Return the new prefab
		return clone;
	}

	/**
	 * Return the next RecycleElement in the pool. If one does not exist, create one
	 * @param pos a Vector3 position to place the RecycleElement at
	 * @return the RecycleElement that was created
	 */
	public RecycleElement NextElement(Vector3 pos) {
		RecycleElement instance = null;

		// Search the pool for an element that is not active
		foreach (var go in poolInstances) {
			if (go.gameObject.activeSelf != true) {
				instance = go;
				instance.transform.position = pos;
			}
		}

		// If none was found, create one using CreateInstance
		if(instance == null) {
			instance = CreateInstance (pos);

			// Start the instance if it is new
			instance.Startup ();
		} else {

			// Restart the instance if it is reused
			instance.Restart ();
		}

		return instance;
	}
}