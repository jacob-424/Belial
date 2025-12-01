/*
 Author: Jacob Wiley
 Date: 11/30/2025
 Description: Blaster controller
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
    private float seconds; // Time in seconds since the last bullet fired
    [SerializeField] float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        seconds = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        leftSpawn = transform.TransformPoint(new Vector3(-0.755f, -0.15f, 0));
        rightSpawn = transform.TransformPoint(new Vector3(0.755f, -0.15f, 0));

        // Fire blaster when left mouse button is held and enough time has passed since the last bullet
        if (Input.GetMouseButton(0) && seconds >= fireRate) {
            Instantiate(bullet, leftSpawn, transform.rotation);
            Instantiate(bullet, rightSpawn, transform.rotation);
            seconds = 0f;
        }
    }
}
