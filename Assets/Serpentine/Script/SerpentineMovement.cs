using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentineMovement : MonoBehaviour
{
    //input parameters
    float horizontal;

    //movement and rotation parameters
    [Header("movement and rotation parameters")]
    [SerializeField] private float movement_speed;
    [SerializeField] private float rotation_speed;

    //surface allignment parameters
    [Header("surface allignment parameters")]
    [SerializeField] private float radius;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask groundMask;
    RaycastHit hit_down;

    //climbing parameters
    [Header("climbing parameters")]
    [SerializeField] private float wallDetectionLength;
    [SerializeField] private float wallRadius;
    private RaycastHit frontWallHit;
    [SerializeField] private bool wallFront;

    // Update is called once per frame
    void Update()
    {
        //calling functions
        inputMapper();
        rotationController();
        surfaceAllign();            
        movementController();
    }

    void inputMapper()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    void movementController()
    {
        transform.position += transform.forward * movement_speed * Time.deltaTime;
    }

    void rotationController()
    {
        if(wallCheck()){
            Vector3 surfaceNormal = frontWallHit.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation; 
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f);
            transform.rotation = targetRotation;
        }

        transform.Rotate(Vector3.up * horizontal * rotation_speed * Time.deltaTime);
    }

    void surfaceAllign()
    {
            // transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, hit_down.point.y, Time.deltaTime * 4f), transform.position.z);
        if(Physics.SphereCast(transform.position, radius, -transform.up, out hit_down, distance, groundMask)){
            Vector3 surfaceNormal = hit_down.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            transform.rotation = targetRotation;
        }
    }

    //climbing related functions
    bool wallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, wallRadius, transform.forward, out frontWallHit, wallDetectionLength, groundMask);
        return wallFront;
    }

    //ray visualization
    void OnDrawGizmos()
    {
        //ground allignment gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (-transform.up * distance), radius);

        Gizmos.color = Color.green;
        Vector3 ground_direction = transform.TransformDirection(Vector3.up) * -distance;
        Gizmos.DrawRay(transform.position, ground_direction);

        //wall detection gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * wallDetectionLength, wallRadius);

        // Gizmos.color = Color.blue;
        Vector3 wall_direction = transform.TransformDirection(Vector3.forward) * wallDetectionLength;
        Gizmos.DrawRay(transform.position, wall_direction);
    }
}
