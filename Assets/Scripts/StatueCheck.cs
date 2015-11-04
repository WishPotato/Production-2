﻿using UnityEngine;
using System.Collections;

public class StatueCheck : MonoBehaviour {
	
	public GameObject correctStatue = null;
	private GameObject curStatue = null;

	// Use this for initialization
	void Start () {

		if (this.gameObject.name == "StatueSpot (2)") {
			placeStatue (GameObject.Find ("Lv2_Statue_VermillionBird"));
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void placeStatue (GameObject statue)
	{
		statue.transform.position = this.transform.position;
		statue.transform.rotation = this.transform.rotation;
		curStatue = statue;
		if (curStatue.name == this.correctStatue.name)
			GameObject.Find ("lvl2_Chinese_Gate").GetComponent<Level2CaveDoor> ().correctStatues++;
	}

	public void removeStatue ()
	{
		Debug.Log (curStatue.ToString() + "  " + correctStatue.ToString());
		if (curStatue.name == correctStatue.name)
			GameObject.Find ("lvl2_Chinese_Gate").GetComponent<Level2CaveDoor> ().correctStatues--;
		GameObject mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		mainCamera.GetComponent<RayCast> ().setStoredPickUpItem (curStatue);
		GameObject arm = GameObject.FindGameObjectWithTag ("Arm");
		arm.GetComponent<ArmsScript>().PickUpItem(curStatue);
		arm.GetComponent<ArmsScript>().IsCarryingItem = true;
		curStatue.GetComponent<Pickable>().CanPickUp = false;
		curStatue = null;
	}

	public GameObject switchStatue(GameObject statueIn)
	{
		if (curStatue.name == this.correctStatue.name)
			GameObject.Find ("lvl2_Chinese_Gate").GetComponent<Level2CaveDoor> ().correctStatues--;
		GameObject temp = curStatue;
		GameObject arm = GameObject.FindGameObjectWithTag ("Arm");
		arm.GetComponent<ArmsScript>().RemoveItem(statueIn);
		arm.GetComponent<ArmsScript>().PickUpItem(temp);
		placeStatue (statueIn);
		return temp;
	}

	public bool isEmpty()
	{
		if (curStatue == null) 
			return true; 
		else 
			return false;
	}
}
