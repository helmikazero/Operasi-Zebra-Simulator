using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public Rigidbody playerRigid;
    public float speed = 100f;
    public float jumpForce = 10f;
    public int jumpLimit = 1;
    public AudioSource walkSfx;


    public float movementX;
    public float movementZ;

    // Start is called before the first frame update
    void Start()
    {
        /*initialTransform = walkingTransform.localPosition;*/
    }

    private void FixedUpdate()
    {
        playerRigid.velocity = playerRigid.transform.TransformDirection(movementX * speed * Time.deltaTime, playerRigid.velocity.y, movementZ * speed * Time.fixedDeltaTime);
        //Movement();
    }

    // Update is called once per frame
    void Update()
    {

        MovementInput();
    }

    void Movement()
    {
        playerRigid.AddForce(playerRigid.transform.forward * movementZ * Time.deltaTime);
        playerRigid.AddForce(playerRigid.transform.right * movementX * Time.deltaTime);

    }

    private void MovementInput()
    {
        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        //playerRigid.velocity = playerRigid.transform.TransformDirection(Input.GetAxis("Horizontal") * speed, playerRigid.velocity.y, Input.GetAxis("Vertical") * speed);

        if (jumpLimit > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpLimit--;
                playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpForce, playerRigid.velocity.z);
            }

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")  /*Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)*/)
            {
                /*amount += goyangSpeed * Time.deltaTime;
                walkingTransform.localPosition = new Vector3(walkingTransform.localPosition.x, Mathf.Sin(amount) / pembagi, Mathf.Sin(amount) / pembagi);*/
                //Debug.Log(Mathf.Cos(amount));
                if (!walkSfx.isPlaying)
                {
                    walkSfx.Play();
                }
            }
            else
            {
                walkSfx.Stop();
                /*walkingTransform.localPosition = Vector3.Slerp(walkingTransform.localPosition, initialTransform, recover * Time.deltaTime);
                
                amount = 0;*/
            }
        }
        else
        {
            walkSfx.Stop();
            /*amount = 0;*/
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        jumpLimit = 1;

    }
}
