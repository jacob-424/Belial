/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Prevents the final frame of the explosions from lingering
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy once the animation is finished
        // Must be in update so it does not execute until active
        Destroy(gameObject, 5.2f);
    }
}
