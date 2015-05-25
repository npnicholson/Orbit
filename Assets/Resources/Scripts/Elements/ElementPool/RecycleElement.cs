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

/**
 * This script allows an object to be recycled and used by the ElementUtil.
 * When an Element is created, the Startup() method is called. From then on,
 * the Restart() and Shutdown() methods are called on every other component
 * that impliments IRecycle
 * 
 * @author Norris Nicholson
 * @version 1.0
 * 
 * This code is partially based on the Lynda.com course, Unity 5 2D essentials
 * 
 */
public class RecycleElement : MonoBehaviour {
	
	private List<IRecycle> recycleComponents;

	/**
	 * Search the attached gameObject for components and add them
	 */
	void Awake() {
		//if(gameObject.GetComponents<Element>() == null)
			gatherComponents();
	}

	/**
	 * Restart this GameObject by calling Restart() on all scripts that
	 * extend IRecycle
	 */
	public void Restart() {
		//gatherComponents();
		MonoBehaviour.print("Recycle Element Restart");
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
		MonoBehaviour.print("Recycle Element Shutdown");
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
		//gatherComponents();
		MonoBehaviour.print("Recycle Element Startup");
		// Set the GameObject to inactive
		gameObject.SetActive (true);
		
		// Run Startup() on each script in RecycleComponents
		foreach (var component in recycleComponents) {
			component.Startup ();
		}
	}

	public void gatherComponents() {
		var componenets = GetComponents<MonoBehaviour> ();
		recycleComponents = new List<IRecycle> ();
		
		// Find all of the componenets that extend IRecycle
		// and add them to the recycleComponenets list
		print("Components that have been added: ");
		foreach (var component in componenets) {
			//print(component);
			if(component is IRecycle) {
				recycleComponents.Add (component as IRecycle);
				print(component);
			}
		}
		print("-------------------");
	}

}