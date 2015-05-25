using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	private int count = 50;
	private bool isShip = false;
	private GameObject ship;
	private GameObject earth;

	private System.Type[] addOns;

	private System.Type shipType;

	public Sprite shipSprite;
	public Sprite earthSprite;
	// Update is called once per frame

	void Awake() {
		//ship = (GameObject) Resources.Load("ship", typeof(GameObject));
		ship = (GameObject)Resources.Load("Prefabs/ship");

		earth = (GameObject)Resources.Load("Prefabs/earth");

		//world = GameObject.Find("World");
		if(ship == null || earth == null)
			print ("Error: Ship or earth prefab does not exist");


		shipType = typeof(Ship);

		addOns = new System.Type[1];
		
		addOns[0] = shipType;
	}

	void Update () {
		count ++;

		if(count > 40) {
			count = 0;
			isShip = !isShip;
			//print("New object");
			if(isShip) {

				// @TODO Add components to this array
				//addOns[0] = Type

				//foreach(Component comp in addOns)
				//	print(Ship.GetType());

				ObjectUtil.addObject(shipSprite, addOns, "ship");
				//ElementUtil.Instantiate(ship, ElementUtil.holdingPos + ElementUtil.getWorldPos());
			}else {
				ObjectUtil.addObject(earthSprite, addOns, "earth");
			}
		}
	}
}
