using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField, Tooltip("Speed of Player movement")]
    private float speed;

    [SerializeField, Tooltip("Speed of Player rotation")]
    private float rotateSpeed;

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //--------Movement--------
        //Get input from the X and the Z axis
        //Give it to the animator to determine which animation to play
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input *= speed;
        input = transform.InverseTransformDirection(input);
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.z);

        //Spriting
        //Holding down shift increases the speed to 5 and resets back to 3 once released
        //Walking/Sprinting animations are automatically determined using the blend tree
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5f;
        }
        else
        {
            speed = 3f;
        }

        //--------Rotation--------
        //Get a raycast from the mouse
        //Create a plane and find the point where the raycast intersects the plane
        //Use quaternions to gradually rotate the character to face the intersection point
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if(plane.Raycast(ray, out distance))
        {
            Quaternion rotateTarget = Quaternion.LookRotation(ray.GetPoint(distance) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTarget, rotateSpeed * Time.deltaTime);
        }
    }
}
