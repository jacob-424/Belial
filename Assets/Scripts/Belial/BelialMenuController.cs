/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Controls Belial's movement on the menu screen
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BelialMenuController : MonoBehaviour
{
    // Coordinates for the points that Belial can travel to
    private int[] xCoords = { -12, -9, -6, -3, 0, 3, 6, 9, 12 };
    private int[] yCoords = { -6, -3, 0, 3, 6 };

    private Vector3 targetPos;
    private Vector3 startPos;

    [SerializeField] float moveSpeed;
    private Rigidbody2D rb2d;
    private float timeElapsed; // Tracks time between movements

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timeElapsed = 0;

        // Moving initialization
        targetPos = (Vector3)rb2d.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Moving();
    }

    // Moves Belial around the screen
    void Moving()
    {
        if ((Vector3)rb2d.position == targetPos && timeElapsed >= 1.2f) // Pick new target position after 1.2 second delay
        {
            // Generate a new target postion for Belial to move to
            targetPos = new Vector3(xCoords[Random.Range(0, xCoords.Length)], yCoords[Random.Range(0, yCoords.Length)]);
            timeElapsed = 0f;
        }
        else if ((Vector3)rb2d.position == targetPos) // Wait 1.2 second delay
        {
            timeElapsed += Time.fixedDeltaTime;
        }

        rb2d.MovePosition(Vector3.MoveTowards(rb2d.position, targetPos, moveSpeed * Time.fixedDeltaTime));
    }
}
