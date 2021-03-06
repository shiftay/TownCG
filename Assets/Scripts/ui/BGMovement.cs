﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour {

	public GameObject objectToMove;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		objectToMove.GetComponent<Rigidbody2D>().AddForce((Vector2.left * -speed));
	}



	void OnTriggerEnter2D(Collider2D other)
	{
		other.gameObject.GetComponent<TrailFix>().ChangeObject();
		speed *= -1;
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		other.gameObject.GetComponent<TrailFix>().ChangeObject();
		speed *= -1;
	}



}



