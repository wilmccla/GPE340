using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public Transform target;

    private Vector3 position;

    void Update()
    {
        //Get the position of the target
        //Lock the Y position to what it currently is
        //Gradually move towards the target at the given speed
        position = target.position;
        position.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }
}
