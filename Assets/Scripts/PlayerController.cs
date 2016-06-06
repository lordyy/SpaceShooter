using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax; 
}

public class PlayerController : MonoBehaviour 
{
	private Rigidbody rb;
	private AudioSource shot_audio;
	private float next_fire;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shot_spawn;
	public float fire_rate;



	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		shot_audio = GetComponent<AudioSource> ();
	} 

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > next_fire)
		{
			next_fire = Time.time + fire_rate;
			Instantiate (shot, shot_spawn.position, shot_spawn.rotation);
			shot_audio.Play ();
		}
	}

	void FixedUpdate()
	{
		float move_horizontal = Input.GetAxis ("Horizontal");
		float move_vertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (move_horizontal, 0.0f, move_vertical);

		rb.velocity = movement * speed;
		rb.position = new Vector3 
			(
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

	}

}﻿