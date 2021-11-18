//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform[] Lanes;
    public int CurrentLane = 1;
    public float Speed = 20f;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.W))
    //    {
    //        if (CurrentLane > 0)
    //        {
    //            CurrentLane--;
    //            Vector3 pos = transform.position;
    //            pos.y = Lanes[CurrentLane].position.y;
    //            transform.position = pos;
    //        }
    //    }
    //    else if (Input.GetKeyUp(KeyCode.S))
    //    {
    //        if (CurrentLane < 2)
    //        {
    //            CurrentLane++;
    //            Vector3 pos = transform.position;
    //            pos.y = Lanes[CurrentLane].position.y;
    //            transform.position = pos;
    //        }
    //    }
    //    rb.velocity = Vector2.right * Speed * Time.deltaTime;
    //}

    float currentSpeed = 0f;
    float maxSpeed = 10f;
    public float movementSpeed = 5.0f;
    public GameObject player;
    private float screenCenterX;
    // New variables :
    public float accelerationTime = 60;
    private float minSpeed;
    private float time;

    private void Start()
    {
        // save the horizontal center of the screen
        screenCenterX = Screen.width * 0.5f;
        minSpeed = currentSpeed;
        time = 0;
    }


    private void Update()
    {
        // https://docs.unity3d.com/ScriptReference/Mathf.SmoothStep.html
        currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);
        transform.position -= -transform.right * currentSpeed * Time.deltaTime;
        time += Time.deltaTime;
        // ....
    }
}
