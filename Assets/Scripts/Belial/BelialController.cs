/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Controls Belial's movement, attack phases, and health
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BelialController : MonoBehaviour
{
    private SmallBelial smallBelial;
    [SerializeField] GameObject outline; // Red outline for flashing and dashing phases
    [SerializeField] GameObject explosion; // Explosion death animation
    [SerializeField] GameObject player;

    internal static int health;
    [SerializeField] Text healthText;

    // Belial phases
    internal enum State { 
        starting,
        moving,
        flashing,
        dashing
    }
    internal static State state;

    // Coordinates for the points that Belial can travel to during moving phase
    private int[] xCoords = { -12, -9, -6, -3, 0, 3, 6, 9, 12 };
    private int[] yCoords = { -6, -3, 0, 3, 6 };

    private Vector3 targetPos;
    private Vector3 startPos;

    [SerializeField] float dashSpeed;
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb2d;
    private float timeElapsed;
    private int fireCount; // Small belial fire count for moving phase
    private int flashCount; // Outline flash count for flashing phase

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        smallBelial = GetComponent<SmallBelial>();
        outline.SetActive(false);
        explosion.SetActive(false);
        timeElapsed = 0;
        fireCount = 0;
        flashCount = 0;
        health = 1000;

        // Initial phase
        state = State.starting;

        // Moving phase initialization
        startPos = rb2d.position;
    }

    // Manages starting/flashing phases and health UI
    void Update()
    {
        if (health <= 0) {
            GameController.gameWon = true;
            gameObject.SetActive(false);

            // Explosion death animation
            explosion.transform.position = rb2d.position;
            explosion.SetActive(true);

            AudioController.PlayExplosion(); // SFX
            healthText.text = "Belial Health: " + 0; // UI
        }
        else healthText.text = "Belial Health: " + health; // UI

        if (state == State.starting)
        {
            Starting();
        }
        
        if (state == State.flashing) {
            Flashing();
        }
    }

    // Manages moving and dashing phases
    void FixedUpdate()
    {
        if (state == State.moving)
        {
            Moving();
        }
        else if (state == State.dashing) {
            Dashing();
        }
    }

    // Adds a 1.2 second delay before entering moving phase
    // Occurs only once at the start
    void Starting()
    {
        // Count to 1.2 seconds
        if (timeElapsed >= 1.2f)
        {
            state = State.moving; // Transition to moving phase
            timeElapsed = 0;
        }
        else timeElapsed += Time.deltaTime;
    }

    // Moves Belial to a new position while firing two bursts of small Belials
    void Moving()
    {
        if ((Vector3)rb2d.position == startPos) // Moving phase initialization
        {
            // Generate a new target postion for Belial to move to
            targetPos = new Vector3(xCoords[Random.Range(0, xCoords.Length)], yCoords[Random.Range(0, yCoords.Length)]);
        }
        else if ((Vector3)rb2d.position == targetPos) // Transition to next phase
        {
            state = State.flashing;
            fireCount = 0;
            return;
        }

        float distance = Vector3.Distance(startPos, targetPos); // Distance to target position
        float traveledDistance = Vector3.Distance(startPos, rb2d.position); // Total distance traveled from start position
        float percent = traveledDistance / distance; // Percentage to the target position

        // Fire a small belial when Belial is 25% and 75% to the target position
        if (percent >= 0.25f && fireCount == 0)
        {
            smallBelial.FireSmallBelial();
            fireCount++;
        }
        else if (percent >= 0.75f && fireCount == 1)
        {
            smallBelial.FireSmallBelial();
            fireCount++;
        }

        rb2d.MovePosition(Vector3.MoveTowards(rb2d.position, targetPos, moveSpeed * Time.fixedDeltaTime));
    }

    // Periodically flashes the red outline to alert the player of the incoming dash attack
    void Flashing() 
    {
        // Flash red outline every 500ms for 300ms, 5 times
        switch(flashCount)
        {
            case 0:
                Flash(0.5f, 0.8f);
                break;

            case 1:
                Flash(1.3f, 1.6f);
                break;

            case 2:
                Flash(2.1f, 2.4f);
                break;

            case 3:
                Flash(2.9f, 3.2f);
                break;

            case 4:
                Flash(3.7f, 4f);
                break;
        }

        // Transition to dashing phase once all flashes are finished
        if (timeElapsed >= 4.5f)
        {
            state = State.dashing;
            timeElapsed = 0;
            flashCount = 0;
            targetPos = player.transform.position;
            return;
        }

        timeElapsed += Time.deltaTime;
    }

    // Turns the flash outline on if the total elapsed time is between startTime and endTime
    void Flash(float startTime, float endTime) 
    {
        if (timeElapsed >= startTime && timeElapsed < endTime) // Show outline
        {
            outline.SetActive(true);
        }
        else // Do not show outline
        {
            outline.SetActive(false);

            // Check if the flash has finished
            if (timeElapsed > endTime)
            {
                flashCount++;
            }
        }
    }

    // Perform a dash attack at the player dealing damage
    void Dashing()
    {
        outline.SetActive(true);

        // Check if the dash has finished
        if ((Vector3)rb2d.position == targetPos)
        {
            outline.SetActive(false);

            
            if (timeElapsed < 1f) // Wait 1 second before transitioning to next phase
            {
                timeElapsed += Time.fixedDeltaTime;
            }
            else // Transition to move phase
            {
                state = State.moving;
                timeElapsed = 0;
                startPos = rb2d.position;
                return;
            }
        }

        rb2d.MovePosition(Vector3.MoveTowards(rb2d.position, targetPos, dashSpeed * Time.fixedDeltaTime));
    }

    // Detect when Belial is hit by a bullet or missile
    void OnTriggerEnter2D(Collider2D other) {
        
        // Bullet collision
        if (other.gameObject.CompareTag("Bullet")) {
            other.gameObject.SetActive(false);
            health -= 2;
        }

        // Missile collision
        if (other.gameObject.CompareTag("Missile"))
        {
            other.gameObject.SetActive(false);
            health -= 50;
            AudioController.PlayMissileExplosion(); // SFX
        }
    }
}
