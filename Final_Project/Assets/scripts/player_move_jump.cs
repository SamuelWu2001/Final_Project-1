using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move_jump : MonoBehaviour {
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
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDirection = new Vector3(h, 0, v);
        if (Input.GetKey(KeyCode.W))
        {
            maindirection = speed * Time.deltaTime * transform.forward;
            rb.MovePosition(rb.position + maindirection * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, new Vector3(0, -1, 0), Time.deltaTime * rotate_speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 opDirection = new Vector3(0f, -180f, 0f);
            transform.Rotate(opDirection);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, new Vector3(0, 1, 0), Time.deltaTime * rotate_speed);
        }
        if (Input.GetKey(KeyCode.RightShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            speed = 3;
        }
        else
            speed = 2;
        if (Input.GetKeyDown(KeyCode.Space) && a < 1)
        {
            rb.AddForce(jumpdirection * jumpforce);
            a += 2;
        }

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
            animator.SetInteger("AnimationPar", 1);
        else
            animator.SetInteger("AnimationPar", 0);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
            a = 0;
    }
}
