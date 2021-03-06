﻿using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed = 7.0f;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;

    public AudioClip collectSound;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

	private void Start() {
		controller = GetComponent<CharacterController>();
	}
	
	private void Update() {
		if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
 
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void FixedUpdate() {
        //
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Pill") {
            return;
        }
        Destroy(other.gameObject);
        GameState.Instance.PixelLevel = Mathf.Min(1024, GameState.Instance.PixelLevel + 50);
        GetComponent<AudioSource>().PlayOneShot(collectSound, 0.7f);
    }

}
