/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Controls small Belial movement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBelialController : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        transform.Translate(direction * speed, Space.Self);
    }

    // Sets the direction for the small Belial to travel in
    public void Intialize(Vector3 direction) 
    {
        this.direction = direction;
    }
}
