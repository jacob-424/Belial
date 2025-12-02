using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BelialController : MonoBehaviour
{
    private SmallBelial smallBelial;

    enum State { 
        moving,
        flashing,
        dashing
    }
    private State state;

    private int[] xPoints = { -12, -9, -6, -3, 0, 3, 6, 9, 12 };
    private int[] yPoints = { -6, -3, 0, 3, 6 };
    private Vector3 targetPos;
    private Vector3 startPos;
    private float moveDuration = 3f;
    private float speed;
    private Rigidbody2D rb2d;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        smallBelial = GetComponent<SmallBelial>();
        timeElapsed = 0;
        state = State.moving;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (state == State.moving) {
            float t = timeElapsed / moveDuration;
            if (timeElapsed == 0) {
                targetPos = new Vector3(xPoints[Random.Range(0, xPoints.Length)], yPoints[Random.Range(0, yPoints.Length)]);
                startPos = transform.position;
            }
            else if (timeElapsed >= moveDuration) {
                state = State.moving;
                timeElapsed = 0;
                rb2d.MovePosition(targetPos);
                return;
            }

            if (Mathf.Approximately(t, 0.3f)) {
                smallBelial.FireSmallBelial();
            }

            Debug.Log(t);
            rb2d.MovePosition(Vector3.Lerp(startPos, targetPos, t));
            timeElapsed += Time.fixedDeltaTime;
        }
    }
}
