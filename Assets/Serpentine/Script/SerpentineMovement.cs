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
    [SerializeField] private LayerMask detectableMask;
    RaycastHit hit_down;

    // Update is called once per frame
    void Update()
    {
        //calling functions
        inputMapper();
        movementController();
        rotationController();
        surfaceAllign();
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
        if(Physics.SphereCast(transform.position + Vector3.up * 5, radius, -transform.up, out hit_down, distance, detectableMask)){
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, hit_down.point.y, Time.deltaTime * 4f), transform.position.z);
        }

    }

    //ray visualization
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hit_down.point, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(hit_down.point, hit_down.normal);
    }
}
