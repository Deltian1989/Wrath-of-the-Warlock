using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int walkSpeed = 6;

    private Vector2 moveInput;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        moveInput = callbackContext.ReadValue<Vector2>();
    }

    private void Update()
    {
        moveInput.Normalize();
        _characterController.Move(new Vector3(moveInput.x, 0, moveInput.y) * walkSpeed * Time.deltaTime);
    }
}
