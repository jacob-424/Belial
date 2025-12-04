/*
 Author: Jacob Wiley
 Date: 11/30/2025
 Description: Controls player movement
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject explosion;
    [SerializeField] Text healthText;
    private Rigidbody2D rb2d;

    internal static int health;
    private bool invulnerable;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        invulnerable = false;
        explosion.SetActive(false);
        timeElapsed = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Player rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameController.gameOver = true;

            // Explosion animation
            explosion.transform.position = rb2d.position;
            explosion.SetActive(true);

            // Explosion audio
            AudioController.PlayExplosion();
            
            healthText.text = "Health: 0";
        }
        else healthText.text = "Health: " + health;

        // Count 2 seconds of invulnerability then make the player vulnerable again
        if (invulnerable && timeElapsed >= 2f)
        {
            invulnerable = false;
            timeElapsed = 0;
        }
        else if (invulnerable)
        {
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
            AudioController.PlayDamage();
            health -= 10;
        }

        // Tale 15 points of damage if hit by Belial while he is dashing
        if (other.gameObject.CompareTag("Belial") && BelialController.state == BelialController.State.dashing && !invulnerable) 
        {
            invulnerable = true; // Grant 2 seconds of invulnerability
            AudioController.PlayDamage();
            health -= 15;
        }
    }
}
