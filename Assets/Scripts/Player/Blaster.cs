using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private Vector3 leftSpawn;
    private Vector3 rightSpawn;
    private float timePassed;
    [SerializeField] float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        leftSpawn = transform.TransformPoint(new Vector3(-0.755f, -0.15f, 0));
        rightSpawn = transform.TransformPoint(new Vector3(0.755f, -0.15f, 0));

        if (Input.GetMouseButton(0) && timePassed >= fireRate) {
            Instantiate(bullet, leftSpawn, transform.rotation);
            Instantiate(bullet, rightSpawn, transform.rotation);
            timePassed = 0f;
        }
    }
}
