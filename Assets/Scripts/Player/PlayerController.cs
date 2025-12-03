/*
 Author: Jacob Wiley
 Date: 11/30/2025
 Description: Controls player movement
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb2d;

    internal static int health = 100;
    private bool invulnerable;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        invulnerable = false;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Player rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        // Count 2 seconds of invulnerability then make the player vulnerable again
        if (invulnerable && timeElapsed >= 2f)
        {
            invulnerable = false;
            timeElapsed = 0;
        }
        else if (invulnerable) {
            timeElapsed += Time.deltaTime;
        }
    }

    void FixedUpdate() {
        // WASD/Arrow key movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb2d.MovePosition(rb2d.position + new Vector2(moveHorizontal, moveVertical) * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Take 10 points of damage if hit by a small Belial
        if (other.gameObject.CompareTag("SmallBelial") && !invulnerable)
        {
            other.gameObject.SetActive(false);
            health -= 10;
        }

        // Tale 15 points of damage if hit by Belial while he is dashing
        if (other.gameObject.CompareTag("Belial") && BelialController.state == BelialController.State.dashing && !invulnerable) 
        {
            health -= 15;
            invulnerable = true; // Grant 2 seconds of invulnerability after being hit
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
            // Play explosion animation and sound
            // call gamecontroller gameover function
        }

        Debug.Log(health);
    }
}
