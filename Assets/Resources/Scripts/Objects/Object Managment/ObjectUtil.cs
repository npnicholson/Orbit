using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectUtil {

	private static List<GameObject> elements = new List<GameObject>();

	public static GameObject addObject(Sprite sprite, System.Type[] components, string name) {

		GameObject newObject = ElementUtil.Instantiate(ElementUtil.getElement(), ElementUtil.holdingPos);

		if (newObject.GetComponent<Element>() == null) {
			newObject.AddComponent<Element>();
		}

		newObject.name = name;

		newObject.GetComponent<Element>().init(sprite, components);

		elements.Add(newObject);

		return newObject;
	}

	public static bool removeObject(GameObject element) {
		ElementUtil.Destroy(element);
		return true;
	}

	public static bool removeObject(string name) {
		foreach(GameObject element in elements) {
			if(element.name.Equals(name)) {
				ElementUtil.Destroy(element);
				return true;
			}
		}
		return false;
	}

	public static bool removeAll() {
		foreach(GameObject element in elements) {
			removeObject(element);
		}
		return true;
	}

	public static bool removeAll(string name) {
		bool success = false;
		foreach(GameObject element in elements) {
			if(element.name.Equals(name)) {
				ElementUtil.Destroy(element);
				success = true;
			}
		}
		return success;
	}
}
