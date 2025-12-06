/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Controls the player's blaster weapon
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private Vector3 leftSpawn; // Left blaster position
    private Vector3 rightSpawn; // Right blaster position
    private float timeElapsed; // Time in seconds since the last bullet fired
    [SerializeField] float fireRate; // How often bullets can be fired in seconds

    // Start is called before the first frame update
    void Start()
    {
        // Allows the player to fire immediately without waiting at game start
        timeElapsed = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Set left and right bullet spawn points
        leftSpawn = transform.TransformPoint(new Vector3(-0.755f, -0.15f, 0));
        rightSpawn = transform.TransformPoint(new Vector3(0.755f, -0.15f, 0));

        // Fire blaster when left mouse button is held and enough time has passed since the last bullet
        if (Input.GetMouseButton(0) && timeElapsed >= fireRate) {
            Instantiate(bullet, leftSpawn, transform.rotation);
            Instantiate(bullet, rightSpawn, transform.rotation);
            AudioController.PlayBullet(); // SFX
            timeElapsed = 0f;
        }
    }
}
