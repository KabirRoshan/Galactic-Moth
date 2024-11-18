// This script controls the properties of the asteroid such as speed and direction
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    public float speed = 10.0f;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        // To move astroid right to left set rigidbody velocity negative
        rb.velocity = new Vector2(-speed, 0);

        // Defines the boundaries of the screen on the x and y axis
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    } // Start
	
	// Update is called once per frame
	void Update () {
        // To check if asteroid is to the left of the screen and destroy it
        // the multiply by 2 allows for asteroid to be destroyed outside the screen.
        // Because camera is moving we add the camera position to screenBounds
        // Camera.main.transform.position.x +
        if (transform.position.x < Camera.main.transform.position.x + screenBounds.x * -1)
        {
            Destroy(this.gameObject);
        } // if
    } // Update
} // Class Asteroid
