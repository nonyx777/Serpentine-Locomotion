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
    [SerializeField] private float maxWallLookAngle;
    [SerializeField] private float wallLookAngle;
    private RaycastHit frontWallHit;
    [SerializeField] private bool wallFront;
    [SerializeField] private LayerMask wallMask;


    // Update is called once per frame
    void Update()
    {
        //calling functions
        inputMapper();
        movementController();
        rotationController();
        surfaceAllign();
        wallCheck();
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
        transform.Rotate(Vector3.up * horizontal * rotation_speed * Time.deltaTime);
    }

    void surfaceAllign()
    {
        if(Physics.SphereCast(transform.position + Vector3.up * 5, radius, -transform.up, out hit_down, distance, groundMask)){
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, hit_down.point.y, Time.deltaTime * 4f), transform.position.z);
        }

    }

    //climbing related functions
    void wallCheck()
    {
        wallFront = Physics.SphereCast(transform.position, wallRadius, transform.forward, out frontWallHit, wallDetectionLength, wallMask);
        wallLookAngle = Vector3.Angle(transform.forward, -frontWallHit.normal);
    }

    //ray visualization
    void OnDrawGizmos()
    {
        //ground allignment gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hit_down.point, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(hit_down.point, hit_down.normal);

        //wall detection gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * wallDetectionLength, wallRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, new Vector3(transform.position.x, 0, transform.position.z + wallDetectionLength));
    }
}
