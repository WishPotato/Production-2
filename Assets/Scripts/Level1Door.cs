﻿using UnityEngine;
using System.Collections;

// -----------------------
// Add to the level 1 exit door.
// -----------------------

public class Level1Door : MonoBehaviour {

    public GameObject level1Object;
    public GameObject exitObject;

    private Vector3 slideVector;
    private Vector3 startVector;
	private bool nowOpen = false;
	private bool soundHasPlayed = false;
	private bool doorHasClosedAgain = false;
    public bool puzzleSolved;
    private int doorClosesHopefully;

	void Start () {

        if (level1Object.GetComponent<Pickable>() == null)
        {
            Debug.Log("Pickable.cs is missing in the GameObject!");
        }

        slideVector = new Vector3(this.transform.position.x + 5f, this.transform.position.y, this.transform.position.z);
        startVector = this.transform.position;
	}
	

	void FixedUpdate () {

        if (!level1Object.GetComponent<Pickable>().CanPickUp && !exitObject.GetComponent<ExitPoint>().HasEntered)
        {
			GameObject.Find ("StartupMusicCue").GetComponent<SECTR_PointSource>().Volume -= 0.002f;
            this.transform.position = Vector3.Lerp(this.transform.position, slideVector, 1.45f * Time.deltaTime);
			nowOpen = true;
            puzzleSolved = true;
        }
        else if (exitObject.GetComponent<ExitPoint>().HasEntered)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, startVector, 1.45f * Time.deltaTime);
            nowOpen = false;
			if(!doorHasClosedAgain){
				this.GetComponent<SECTR_PropagationSource>().Play();
				doorHasClosedAgain = true;
            }
        }
        
        if (GameObject.Find("Level1Deleter").GetComponent<ExitPoint>().HasEntered)
        {
            Destroy(GameObject.Find("EntireLevel1"));
        }

        if (nowOpen && !soundHasPlayed)
        {
            this.GetComponent<SECTR_PropagationSource>().Play();
            soundHasPlayed = true;
        }
        
	}
}
