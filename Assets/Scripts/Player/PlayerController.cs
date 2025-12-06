/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Controls player movement, aiming, and health
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject explosion; // Explosion death animation

    internal static int health;
    [SerializeField] Text healthText;

    private Rigidbody2D rb2d;
    private bool isInvulnerable; // Tracks if invulnerable after being hit by a dash
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        explosion.SetActive(false);
        isInvulnerable = false;
        timeElapsed = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate player according to mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        if (health <= 0) // Player lost
        {
            gameObject.SetActive(false);
            GameController.gameOver = true;

            // Explosion death animation
            explosion.transform.position = rb2d.position;
            explosion.SetActive(true);

            AudioController.PlayExplosion(); // SFX
            
            healthText.text = "Health: 0"; // UI
        }
        else healthText.text = "Health: " + health; // UI

        // Count 2 seconds of invulnerability then make the player vulnerable again
        if (isInvulnerable && timeElapsed >= 2f) // 2 seconds have elapsed
        {
            isInvulnerable = false;
            timeElapsed = 0;
        }
        else if (isInvulnerable) // Count to 2 seconds
        {
            timeElapsed += Time.deltaTime;
        }
    }

    void FixedUpdate() {
        // WASD/Arrow key movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb2d.MovePosition(rb2d.position + new Vector2(moveHorizontal, moveVertical) * moveSpeed * Time.fixedDeltaTime);
    }

    // Detect when the player is hit by a small Belial or dash
    void OnTriggerEnter2D(Collider2D other)
    {
        // Take 10 points of damage if hit by a small Belial
        if (other.gameObject.CompareTag("SmallBelial") && !isInvulnerable)
        {
            other.gameObject.SetActive(false);
            AudioController.PlayDamage(); // SFX
            health -= 10;
        }

        // Tale 15 points of damage if hit by Belial while he is dashing
        if (other.gameObject.CompareTag("Belial") && BelialController.state == BelialController.State.dashing && !isInvulnerable) 
        {
            isInvulnerable = true; // Grant 2 seconds of invulnerability
            AudioController.PlayDamage(); // SFX
            health -= 15;
        }
    }
}
