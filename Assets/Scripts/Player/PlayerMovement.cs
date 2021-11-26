using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform[] Lanes;
    public int CurrentLane = 1;
    public float Speed = 20f;

    public float MovementSpeed = 5.0f;
    public GameObject Player;
    public float accelerationTime = 60;
    
    private float currentSpeed = 1f;
    private float maxSpeed = 14f;
    private float minSpeed;
    private float time;
    //end
    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        minSpeed = currentSpeed;
        time = 0;
    }
    private void Update()
    {
        //Go up 1 line when W is unheld
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Limit out of bounds
            if (CurrentLane > 0)
            {
                CurrentLane--;
                Vector3 pos = transform.position;
                pos.y = Lanes[CurrentLane].position.y;
                transform.position = pos;
            }
        }
        //Go down 1 line when S is unheld
        else if (Input.GetKeyUp(KeyCode.S))
        {
            //Limit out of bounds
            if (CurrentLane < 2)
            {
                CurrentLane++;
                Vector3 pos = transform.position;
                pos.y = Lanes[CurrentLane].position.y;
                transform.position = pos;
            }
        }
        //https://answers.unity.com/questions/1342068/increase-speed-from-0f-10f-over-time.html
        currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);
        transform.position -= -transform.right * currentSpeed * Time.deltaTime;
        time += Time.deltaTime;
        //rb.velocity = Vector2.right * Speed * Time.deltaTime;
    }
}
