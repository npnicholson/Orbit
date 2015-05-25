using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Element : MonoBehaviour, IRecycle {

	private Sprite sprite;

	private System.Type[] components = new System.Type[1];
	private int numComps = 0;

	public void Restart() {
		
	}

	public void Startup() {
		// gameObject.GetComponent<RecycleElement>().gatherComponents();
		Restart();
	}

	public void Shutdown() {
		foreach (var comp in components) {
			Destroy (GetComponent(comp));
		}
		components = new System.Type[1];
		numComps = 0;
	}

	public void init(Sprite sprite, System.Type[] comps) {
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = sprite;

		foreach(var comp in comps) {
			if(comp != null) {
				gameObject.AddComponent(comp);
				addComp(comp);
			}
		}
		gameObject.GetComponent<RecycleElement>().gatherComponents();
		gameObject.GetComponent<RecycleElement>().Startup();
	}

	private void addComp(System.Type comp) {
		if (numComps == components.Length) {
			System.Type[] tmp = new System.Type[components.Length + 1];
			for(int i = 0; i < numComps; i++)
				tmp[i] = components[i];

			tmp[components.Length] = comp;

			components = tmp;

		} else {
			components[numComps] = comp;
		}

		numComps++;
	}
}