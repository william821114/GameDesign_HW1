using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {
	public ParticleSystem ps;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(ps)
		{
			if(!ps.IsAlive())
			{
				ChangeLevel ();
			}
		}
	}

	void ChangeLevel(){
		
		if (ApplicationData.isBirdDead && ApplicationData.currentLevel != 2) {
			ApplicationData.rockNumber = 3;
			ApplicationData.currentLevel = 2;
			ApplicationData.isBirdDead = false;

			SceneManager.LoadScene ("Level_2");
		}
	}
}
