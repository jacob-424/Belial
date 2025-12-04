/*
 Author: Jacob Wiley
 Date: 11/30/2025
 Description: Missle launcher controller
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject missile;
    private Vector3 missileSpawn; // Missle spawn position
    private float seconds; // Time in seconds since the last missile was fired
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
        missileSpawn = transform.TransformPoint(new Vector3(0, 0, 0));

        // Fire missile when right mouse button is pressed and enough time has passed since the last missile
        if (Input.GetMouseButtonDown(1) && seconds >= fireRate) {
            Instantiate(missile, missileSpawn, transform.rotation);
            AudioController.PlayMissileLaunch();
            seconds = 0f;
        }
    }
}
