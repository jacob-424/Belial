/*
 Author: Jacob Wiley
 Date: 12/5/2025
 Description: Controls Belial's small Belial attack
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBelial : MonoBehaviour
{
    [SerializeField] GameObject smallBelialPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Fires 12 small Belials in a circle around Belial
    internal void FireSmallBelial() 
    {
        float rad = 0;

        // Fire a small Belial every 30 degrees
        for (int i = 0; i < 12; i++)
        {
            GameObject obj = Instantiate(smallBelialPrefab, transform.position, transform.rotation);
            SmallBelialController controller = obj.GetComponent<SmallBelialController>();
            controller.Intialize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0)); // Set direction

            rad += (Mathf.PI / 6); // Increment by 30 degrees
        }
    }
}
