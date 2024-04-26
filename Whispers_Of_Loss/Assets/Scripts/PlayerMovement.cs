using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    public float groundDrag;
    public PlayerCamera playerCam;
    bool movementDisabled;

    [Header("Headbob")]
    public bool headBob;
    public Transform playerCamera;
    public float walkBobSpeed = 14f;
    public float walkBobAmount = .5f;
    float defaultYPos;
    float timer;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    bool isGrounded;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;
    RandomFootStepSfx footstepSfx => GetComponent<RandomFootStepSfx>();
    bool footstepPlayed;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        defaultYPos = playerCamera.localPosition.y;
    }

    private void Update()
    {
        if(movementDisabled) return;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, groundLayer);
        PlayerInput();
        SpeedControl();
        MovePlayer();
        HeadBob();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection * moveSpeed * 10, ForceMode.Force);
    }

    public void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void HeadBob()
    {
        if (rb.velocity.magnitude <= 0) return;
        if (headBob)
        {
            if (Mathf.Abs(moveDirection.x) > .1f || Mathf.Abs(moveDirection.z) > .1f)
            {
                timer += Time.deltaTime * walkBobSpeed;
                playerCamera.transform.localPosition = new Vector3(
                    playerCamera.transform.localPosition.x,
                    defaultYPos + Mathf.Sin(timer) * walkBobAmount,
                    playerCamera.transform.localPosition.z
                    );
                if (playerCamera.transform.position.y <= 2.25f && !footstepPlayed)
                {
                    footstepPlayed = true;
                    footstepSfx.PlayRandomFootStep();
                }
                else if (playerCamera.transform.position.y > 2.25f)
                {
                    footstepPlayed = false;
                }
            }
        }
    }

    public void DisablePlayerMovement(bool val)
    {
        if (val)
        {
            movementDisabled = true;
            playerCam.enabled = false;
        }
        else
        {
            movementDisabled = false;
            playerCam.enabled = true;
        }
    }
}
