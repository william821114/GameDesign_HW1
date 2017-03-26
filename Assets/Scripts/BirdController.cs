using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {
	public ParticleSystem birdDeadEffect;
	public AudioSource birdDeadSound;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		this.gameObject.SetActive (false);
		birdDeadEffect.transform.position = this.transform.position;
		birdDeadEffect.gameObject.SetActive (true);

		birdDeadSound.Play ();
		ApplicationData.isBirdDead = true;
	}
}
