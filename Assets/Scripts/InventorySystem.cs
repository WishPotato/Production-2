﻿using UnityEngine;
using System.Collections;

// Attach to inventory object
// Lasted Edited: 29-10-2015 10:10

public class InventorySystem : MonoBehaviour {
	
	private GameObject[] bagSlots;
	
	private ArmsScript arms;
	private Movement3D M3;
	private Vector3 handStartPos;
    private Quaternion handDefaultRot;
	
	private GameObject playerObject;
	private GameObject heldBagpackSpace;
	private GameObject equippedBagPackSpace;
	
	private bool hasBagOpen = false;
	private GameObject openSound, closeSound, retreiveSound, storeSound;
	
	void Start () {
		equippedBagPackSpace = GameObject.Find("Equipped Bagpack Space");
		playerObject = GameObject.FindGameObjectWithTag("MainCamera");
		M3 = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement3D>();
		heldBagpackSpace = GameObject.Find("Held Bagpack Space");
		arms = GameObject.Find("Arms").GetComponent<ArmsScript>();
		bagSlots = GameObject.FindGameObjectsWithTag("BagSlot");
		
		for (int i = 0; i < bagSlots.Length; i++)
		{
			if (bagSlots[i] == null)
			{
				Debug.Log("ERROR: Missing Object at Inventory System.");
			}
		}
		openSound = GameObject.Find ("BackpackOpenSound");
		closeSound = GameObject.Find ("BackpackCloseSound");
		retreiveSound = GameObject.Find ("BackpackRetreiveItemSound");
		storeSound = GameObject.Find ("BackpackStoreItemSound");
		
	}
	
	void Update () {
		
		if (M3.HasRisen)
		{
			handStartPos = GameObject.Find("RightArmDefaultPosition").transform.position;
            handDefaultRot = GameObject.Find("RightArmDefaultPosition").transform.rotation;
			
			if (Input.GetButtonDown("PS4_Triangle") || Input.GetKeyDown(KeyCode.I))
			{
				hasBagOpen = !hasBagOpen;
				
				if (!hasBagOpen)
				{
					CloseBag();
				}
				else
				{
					OpenBag();
				}
			}
		}
	}
	
	public void AddItemFromHandToBag(GameObject obj, GameObject bagPos)
	{
		if (hasBagOpen && arms.IsCarryingItem)
		{
            obj.GetComponent<Pickable>().IsInInventory = true;
			obj.transform.parent = bagPos.transform;
			obj.transform.position = bagPos.transform.position;
			arms.IsCarryingItem = false;
			storeSound.GetComponent<SECTR_PointSource>().Play ();
		}
		else
		{
			Debug.Log("You have to open the bag first!");
		}
	}
	
	public void OpenBag()
	{
		// Open the bag
		this.transform.position = heldBagpackSpace.transform.position;
		this.transform.rotation = heldBagpackSpace.transform.rotation;
        arms.rightArm.transform.rotation = handDefaultRot;
		openSound.GetComponent<SECTR_PointSource>().Play ();
	}
	
	public void CloseBag()
	{
		// Close the Bag
		// Reset Right arm position
		arms.rightArm.transform.position = handStartPos;
        arms.rightArm.transform.rotation = handDefaultRot;
		this.transform.position = equippedBagPackSpace.transform.position;
		this.transform.rotation = equippedBagPackSpace.transform.rotation;
		closeSound.GetComponent<SECTR_PointSource>().Play ();
	}
	
	public bool HasBagOpen
	{
		get { return hasBagOpen; }
	}
}
