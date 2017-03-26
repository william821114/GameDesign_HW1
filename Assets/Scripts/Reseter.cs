using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Reseter : MonoBehaviour {
	public Rigidbody2D rock;
	public float resetSpeed = 0.025f;
	public Text endingText;

	private float resetSpeedSqr;
	private SpringJoint2D spring;
	private int currentLevel;

	// Use this for initialization
	void Start () {
		resetSpeedSqr = resetSpeed * resetSpeed;
		spring = rock.GetComponent<SpringJoint2D> ();
		currentLevel = ApplicationData.currentLevel;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			Restart ();

		if (spring == null && rock.velocity.sqrMagnitude < resetSpeedSqr) 
			Reset ();


	}

	void  OnTriggerExit2D (Collider2D other){
		if (other.GetComponent<Rigidbody2D>() == rock) {
			Reset ();
		}

	}

	void Reset()
	{
		if (!ApplicationData.isBirdDead && ApplicationData.rockNumber > 0) {
			if (currentLevel == 1)
				SceneManager.LoadScene ("Level_1");
			else
				SceneManager.LoadScene ("Level_2");
		}

		if (ApplicationData.rockNumber <= 0 && !ApplicationData.isBirdDead) {
			// Game Over	
			endingText.gameObject.SetActive(true);
		}

		if (ApplicationData.isBirdDead && currentLevel == 2) {
			endingText.text = "You Finish The Game";
			endingText.gameObject.SetActive (true);
		}
	}

	void Restart()
	{
		SceneManager.LoadScene ("Level_1");
	}
}
