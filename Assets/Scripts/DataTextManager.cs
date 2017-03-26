using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTextManager : MonoBehaviour {
	private Text dataText;

	// Use this for initialization
	void Start () {
		dataText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		dataText.text = "Level: " + ApplicationData.currentLevel +"\n" + "Rock: " + ApplicationData.rockNumber;
	}
}
