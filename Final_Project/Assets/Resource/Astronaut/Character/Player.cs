using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Controler : MonoBehaviour
{
    private float speed;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] float rotate_speed;
    private Vector3 jumpdirection = new Vector3(0, 1, 0);
    [SerializeField] float jumpforce;
    Rigidbody rb;
    Animator animator;
    private int a = 0;
    private Vector3 maindirection;
    // Use this for initialization

    void Start()
    {
        //transform.position = Vector3.one;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDirection = new Vector3(h, 0, v);
        animator.SetFloat("speed", 0f);
        if (Input.GetKey(KeyCode.W))
        {
            maindirection = speed * Time.deltaTime * transform.forward;
            rb.MovePosition(rb.position + maindirection * speed);
            animator.SetFloat("speed", 1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetFloat("speed", 1f);
            transform.RotateAround(GameObject.Find("Viking_Sword").transform.position, new Vector3(0, -1, 0), Time.deltaTime * rotate_speed);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetFloat("speed", 1f);
            Vector3 opDirection = new Vector3(0f, -180f, 0f);
            transform.Rotate(opDirection);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("speed", 1f);
            transform.RotateAround(transform.localPosition, new Vector3(0, 1, 0), Time.deltaTime * rotate_speed);
        }
        if (Input.GetKey(KeyCode.RightShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            animator.SetFloat("speed", 8f);
            speed = 3;
        }
        else
            speed = 2;
        if (Input.GetKeyDown(KeyCode.Space) && a < 1)
        {
            rb.AddForce(jumpdirection * jumpforce);
            a += 2;
            animator.SetFloat("speed", 12f);
        }
        if (Input.GetKey(KeyCode.P))
            animator.SetFloat("attack", 2f);
        else
            animator.SetFloat("attack", 0f);
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
            a = 0;
    }
}
