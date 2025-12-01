/*
 Author: Jacob Wiley
 Date: 11/30/2025
 Description: Controls missile movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        transform.Translate(new Vector3(0, 1, 0) * speed, Space.Self);
    }
}
