using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private int walkSpeed = 6;

    private Vector2 moveInput;

    private CharacterController characterController;

    private Animator animator;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }

    private void Update()
    {
        moveInput.Normalize();
        characterController.Move(new Vector3(moveInput.x, 0, moveInput.y) * walkSpeed * Time.deltaTime);
        
        bool isRunning = moveInput.sqrMagnitude != 0;

        animator.SetBool("run", isRunning);
    }
}
