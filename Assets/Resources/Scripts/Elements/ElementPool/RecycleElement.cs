using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * The following interface repersents the methods that a recycleElement must
 * contain. 
 */
public interface IRecycle {

	void Startup();
	void Restart();
	void Shutdown();
	
}

public class RecycleElement : MonoBehaviour {
	
	private List<IRecycle> recycleComponents;

	/**
	 * Search the attached gameObject for components and add them
	 */
	void Awake() {
		var componenets = GetComponents<MonoBehaviour> ();
		recycleComponents = new List<IRecycle> ();

		// Find all of the componenets that extend IRecycle
		// and add them to the recycleComponenets list
		foreach (var component in componenets) {
			if(component is IRecycle) {
				recycleComponents.Add (component as IRecycle);
			}
		}
	}

	/**
	 * Restart this GameObject by calling Restart() on all scripts that
	 * extend IRecycle
	 */
	public void Restart() {
		// Set the GameObject to active
		gameObject.SetActive (true);

		// Run Restart() on each script in RecycleComponents
		foreach (var component in recycleComponents) {
			component.Restart ();
		}
	}

	/**
	 * Shutdown this GameObject by calling Shutdown() on all scripts that
	 * extend IRecycle
	 */
	public void Shutdown() {
		// Set the GameObject to inactive
		gameObject.SetActive (false);
		
		// Run Restart() on each script in RecycleComponents
		foreach (var component in recycleComponents) {
			component.Shutdown ();
		}
	}

	/**
	 * Startup this GameObject by calling Startup() on all scripts that
	 * extend IRecycle
	 */
	public void Startup() {
		// Set the GameObject to inactive
		gameObject.SetActive (true);
		
		// Run Restart() on each script in RecycleComponents
		foreach (var component in recycleComponents) {
			component.Startup ();
		}
	}
}