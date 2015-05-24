using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementUtil {

	private static Dictionary<RecycleElement, ElementPool> pools = new Dictionary<RecycleElement, ElementPool> ();

	public static Vector3 holdingPos = new Vector3 (0, 0, 0);

	private static GameObject world;
	private static GameObject element;

	/**
	 * Instantiate a new GameObject using the ElementPool
	 * @param prefab a prefab to instantiate
	 * @pos the initial position in which to place the prefab
	 */
	public static GameObject Instantiate(GameObject obj, Vector3 pos) {

		if(obj == null)
			MonoBehaviour.print("ElementUtil:Instantiate::Null object");

		GameObject instance = null;

		// Search the given object for the RecycleElement script (which allows recycling)
		var recycledScript = obj.GetComponent<RecycleElement> ();

		// If recycledScript is null then the given object is not recyclable
		if (recycledScript != null) {

			// Get the approperate pool for the given object
			var pool = GetElementPool (recycledScript);

			// Get the next available instance in the pool
			instance = pool.NextElement (pos).gameObject;

			// Start the instance (duplicate from Element pool) (Remove this)
			//instance.GetComponent<RecycleElement> ().Startup ();
		} else {

			// Instantiate a new instance of the given object
			instance = GameObject.Instantiate (obj);
			instance.transform.position = pos;
		}

		// return the created (or pooled) instance of the given prafab
		return instance;
	}

	/**
	 * Destroy the given gameObject
	 * @param gameObject the object to be destroyed
	 */
	public static void Destroy(GameObject gameObject) {

		// Search the given object to see if it can be recycled
		var recycleElement = gameObject.GetComponent<RecycleElement> ();
		if (recycleElement != null) {

			// If it can, shut it down through the RecycleElement script
			recycleElement.Shutdown ();
		} else {

			// Otherwise, destroy the gameobject
			GameObject.Destroy (gameObject);
		}
	}

	/**
	 * Gets the ElementPool that has been assigned to the given RecycleElement script
	 * @param reference the RecycleElement to find a pool for
	 * @return An ElementPool that is assigned to the given reference
	 */
	private static ElementPool GetElementPool(RecycleElement reference) {
		ElementPool pool = null;

		// Search the pools dictionary for the given reference
		if (pools.ContainsKey (reference)) {

			// If it exists, then extract the right pool from the dictionary
			pool = pools [reference];
		} else {

			// If it does not, add a new pool using the given reference to the dictionary
			var poolContainer = new GameObject(reference.gameObject.name + ":ElementPool");
			pool = poolContainer.AddComponent<ElementPool>();
			pool.prefab = reference;
			pool.transform.parent = getWorld().transform;
			pools.Add (reference, pool);
		}

		// return the correct pool
		return pool;
	}

	/**
	 * Returns the element game object that ObjectUtil uses
	 * @return the element specified above
	 */
	public static GameObject getElement() {
		if (element == null)
			createElement();
		return element; 
	}

	private static void createElement() {
		element = (GameObject)Resources.Load("Prefabs/element");
		if (element == null) {
			element = new GameObject();
			element.name = "element";
			element.transform.parent = getWorld().transform;

			var comps = new System.Type[] {typeof(Element), typeof(SpriteRenderer), typeof(RecycleElement)};
			foreach (System.Type comp in comps)
				element.AddComponent(comp);
		}
	}

	private static void createWorld() {
		world = GameObject.Find("World");

		if (world == null) {
			world = new GameObject();
			world.name = "World";
		}
	}

	public static GameObject getWorld() {
		if (world == null) 
			createWorld();

		return world;
	}

	public static Vector3 getWorldPos() {
		GameObject world = getWorld();
		return world.transform.position;
	}
}
