using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBelial : MonoBehaviour
{
    [SerializeField] GameObject smallBelialPrefab;

    // Start is called before the first frame update
    void Start()
    {
        FireSmallBelial();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FireSmallBelial() 
    {
        for (float rad = 0; rad < 2 * Mathf.PI; rad += (Mathf.PI / 6)) {
            GameObject obj = Instantiate(smallBelialPrefab, transform.position, transform.rotation);
            SmallBelialController controller = obj.GetComponent<SmallBelialController>();
            controller.Intialize(new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0));
        }
    }
}
