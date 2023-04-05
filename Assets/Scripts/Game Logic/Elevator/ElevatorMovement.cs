using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    /// <summary>
	/// The distance between two floors
	/// </summary>
    [Header("Elevator Logic")]
	public Vector3 FloorDistance = Vector3.up;
    public float Speed = 1.0f;
    public int Floor = 0;
    public int MaxFloor = 1;
    public Transform moveTransform;

    private float tTotal;
    private bool isMoving;
    private float moveDirection;

    [Header("Buttons")]
    [SerializeField]
    private GameObject UpButton;
    [SerializeField]
    private GameObject DownButton;

    public LayerMask layerMask;

    // Use this for initialization
    void Start()
    {
        moveTransform = moveTransform ?? transform;
    }

    private void PressButton()
    {
        Vector3 ScreenCenter = new Vector3(Screen.width / 2,Screen.height / 2, 0);

        Debug.DrawRay(ScreenCenter, Vector3.forward * 100);
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("hit Collider: " + hit.collider.name);

            if (hit.collider.gameObject.CompareTag("ButtonUp"))
            {

                StartMoveUp();
            }
            else if (hit.collider.gameObject.CompareTag("ButtonDown"))
            {
                StartMoveDown();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            PressButton();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            // elevator is moving
            //close doors
            MoveElevator();
        }
        else
        { 
            //open doors
        }
    }

    void MoveElevator()
    {
        var v = moveDirection * FloorDistance.normalized * Speed;
        var t = Time.deltaTime;
        var tMax = FloorDistance.magnitude / Speed;
        t = Mathf.Min(t, tMax - tTotal);
        moveTransform.Translate(v * t);
        tTotal += t;
        print(tTotal);

        if (tTotal >= tMax)
        {
            // we arrived on floor
            isMoving = false;
            tTotal = 0;
            Floor += (int)moveDirection;
            print(string.Format("elevator arrived on floor {0}!", Floor));
        }
    }

    /// <summary>
    /// Start moving up one floor
    /// </summary>
    private void StartMoveUp()
    {
        if (isMoving)
            return;

        isMoving = true;
        moveDirection = 1;
    }

    /// <summary>
    /// Start moving down one floor
    /// </summary>
    private void StartMoveDown()
    {
        if (isMoving)
            return;

        isMoving = true;
        moveDirection = -1;
    }

    /// <summary>
    /// Tell the elevator to move up or down
    /// </summary>
    private void CallElevator()
    {
        if (isMoving)
            return;

        print("elevator starts moving!");

        // start moving
        if (Floor < MaxFloor)
        {
            StartMoveUp();
        }
        else
        {
            StartMoveDown();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
