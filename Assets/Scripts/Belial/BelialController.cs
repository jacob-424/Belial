using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BelialController : MonoBehaviour
{
    private SmallBelial smallBelial;
    [SerializeField] GameObject outline; // Outline that flashes during flashing phase

    // Belial phases
    enum State { 
        moving,
        flashing,
        dashing
    }
    private State state;

    // Coordinates for the points that Belial can travel to during moving phase
    private int[] xCoords = { -12, -9, -6, -3, 0, 3, 6, 9, 12 };
    private int[] yCoords = { -6, -3, 0, 3, 6 };

    private Vector3 targetPos;
    private Vector3 startPos;
    private float moveDuration = 3f;
    private float speed;
    private Rigidbody2D rb2d;
    private float timeElapsed;
    private int fireCount; // Number of times small belial's have been fired
    private int flashCount;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        smallBelial = GetComponent<SmallBelial>();
        outline.SetActive(false);
        timeElapsed = 0;
        fireCount = 0;
        flashCount = 0;
        state = State.moving;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.flashing) {
            Flashing();
        }
    }

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

    // Moves Belial to a target position while firing small Belials
    void Moving() 
    {
        if (timeElapsed == 0) // Start move phase
        {
            // Generate a new target postion for Belial to move to
            targetPos = new Vector3(xCoords[Random.Range(0, xCoords.Length)], yCoords[Random.Range(0, yCoords.Length)]);
            startPos = transform.position;
        }
        else if (timeElapsed >= moveDuration) // Transition to next phase
        {
            state = State.flashing;
            timeElapsed = 0;
            fireCount = 0;
            rb2d.MovePosition(targetPos);
            return;
        }

        float t = timeElapsed / moveDuration;

        // Fire a small belial when Belial is 25% and 75% to the target position
        if (t >= 0.25f && fireCount == 0)
        {
            smallBelial.FireSmallBelial();
            fireCount++;
        }
        else if (t >= 0.75f && fireCount == 1)
        {
            smallBelial.FireSmallBelial();
            fireCount++;
        }

        rb2d.MovePosition(Vector3.Lerp(startPos, targetPos, t));
        timeElapsed += Time.fixedDeltaTime;
    }

    // Periodically flashes the outline to alert the player of the incoming dash attack
    void Flashing() 
    {
        // Flash every 500ms for 300ms, 5 times
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
            return;
        }

        timeElapsed += Time.deltaTime;
    }

    // Turns the flash outline on if the total elapsed time is between startTime and endTime
    void Flash(float startTime, float endTime) 
    {
        if (timeElapsed >= startTime && timeElapsed < endTime)
        {
            outline.SetActive(true);
        }
        else
        {
            outline.SetActive(false);

            // Check if the flash has finished
            if (timeElapsed > endTime)
            {
                flashCount++;
            }
        }
    }

    // Perform a dash attack at the player
    void Dashing()
    {
        
    }
}
