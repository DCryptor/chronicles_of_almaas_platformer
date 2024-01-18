using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class boy_motions : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool motion;
    [SerializeField]
    private float jump;

    [SerializeField]
    private Vector3 offset1;
    [SerializeField]
    private Vector3 offset2;
    [SerializeField]
    private Vector3 offset3;
    [SerializeField]
    private float distanceRay1;
    [SerializeField]
    private float distanceRay2;
    [SerializeField]
    private bool climb1;
    [SerializeField]
    private bool climb2;
    [SerializeField]
    private bool climb3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position + offset1, transform.forward, Color.red, distanceRay1);
        Debug.DrawRay(transform.position + offset2, transform.forward, Color.blue, distanceRay1);
        Debug.DrawRay(transform.position + offset3, -transform.up, Color.green, distanceRay2);

        climb1 = Physics.Raycast(transform.position + offset1, transform.forward, distanceRay1);
        climb2 = Physics.Raycast(transform.position + offset2, transform.forward, distanceRay1);
        climb3 = Physics.Raycast(transform.position + offset3, -transform.up, distanceRay2);

        Vector3 moveDirection = new Vector3(0f, 0f, Input.GetAxis("Horizontal"));
        moveDirection.Normalize();
        rb.velocity = new Vector3(0f, transform.position.y, moveDirection.z * speed );

        if (moveDirection != Vector3.zero)
        {
            motion = true;
            Quaternion rot = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(rb.rotation, rot, 10f * Time.deltaTime);
        }
        else
        {
            motion = false;
        }

        animator.SetBool("motion", motion);
        animator.SetFloat("magnitude", moveDirection.magnitude);

        if (Input.GetKey(KeyCode.Space)) 
        {
            rb.AddForce(transform.up * jump,ForceMode.Impulse);
        }

        if (climb1 && climb2 && climb3)
        {
            animator.SetBool("climb", true);
        }
        else
        {
            animator.SetBool("climb", false);
        }
    }

    private void OnDrawGizmos()
    {

    }
}
