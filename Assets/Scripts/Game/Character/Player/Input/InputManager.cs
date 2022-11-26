using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;

    public bool dashInput; //{get; private set;}
    public bool jumpInput; //{get; private set;}
    public bool meleeAttackInput; //{get; private set;}
    public bool interactionInput; //{get; private set;}
    public Vector2 movementInput; //{get; private set;}

    void Awake() 
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    void Start() 
    {
        DashInputRegister();
        JumpInputRegister();
        MeleeAttackRegister();
        InteractionRegister();
    }


    #region Register

    void DashInputRegister()
    {
        playerControls.Ground.Dash.started += OnDashInput;
        playerControls.Ground.Dash.canceled += OnDashInput;
    }

    void JumpInputRegister()
    {
        playerControls.Ground.Jump.started += OnJumpInput;
        playerControls.Ground.Jump.canceled += OnJumpInput;
    }


    void MeleeAttackRegister()
    {
        playerControls.Ground.MeleeAttack.started += OnMeleeAttackInput;
        playerControls.Ground.MeleeAttack.canceled += OnMeleeAttackInput;
    }
   
    void InteractionRegister()
    {
        playerControls.Ground.Interaction.started += OnInterationInput;
        playerControls.Ground.Interaction.canceled += OnInterationInput;
    }


    #endregion

    #region Input Handle

    public void ResetInput()
    {
        jumpInput = false;
        meleeAttackInput = false;
    }

    private void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            ResetInput();
            dashInput = true;
        }
        
        if (context.canceled) dashInput = false;
    }

    public void UseDashInput()
    {
        dashInput = true;
    }

    void OnJumpInput(InputAction.CallbackContext context) 
    {
        if (context.started) 
        {
            ResetInput();
            jumpInput = true;
        }

        if (context.canceled) jumpInput = false;
    }

    public void UseJumpInput()
    {
        jumpInput = false;
    }

    void OnMeleeAttackInput(InputAction.CallbackContext context) 
    {
        if (context.started) 
        {
            ResetInput();
            meleeAttackInput = true;
        }

        if (context.canceled) meleeAttackInput = false;
    }

    public void UseMeleeAttackInput()
    {
        meleeAttackInput = false;
    }

    void OnInterationInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ResetInput();
            interactionInput = true;
        }

        if (context.canceled) interactionInput = false;
    }

    public void UseInteractionInput()
    {
        interactionInput = false;
    }

    void OnMovementInput()
    {
        movementInput = playerControls.Ground.Move.ReadValue<Vector2>();
    }

    #endregion 

    void Update() 
    {
        OnMovementInput();
    }
}
