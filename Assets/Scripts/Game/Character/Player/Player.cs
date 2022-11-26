using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State

    public IdleState idleState {get; private set;}
    public InAirState inAirState {get; private set;}
    public JumpState jumpState {get; private set;}
    public DashState dashState {get; private set;}
    public HitState hitState {get; private set;}
    public MoveState moveState {get; private set;}
    public MeleeAttackState meleeAttackState {get; private set;}

    #endregion

    #region Animation Clip Data

    int idleId = Animator.StringToHash("Idle");
    int inAirId = Animator.StringToHash("In Air");
    int jumpId = Animator.StringToHash("Jump");
    int dashId = Animator.StringToHash("Dash");
    int meleeAttackId = Animator.StringToHash("Melee Attack");
    int moveId = Animator.StringToHash("Move");
    int hitId = Animator.StringToHash("Hit");

    #endregion

    [SerializeField] PlayerData data;
    StateMachine stateMachine;
    Core core;
    public InputManager inputManager {get; private set;}

    #region Set up
    
    void Awake() 
    {
        inputManager = GetComponent<InputManager>();
        core = GetComponentInChildren<Core>();
    }

    void Start() 
    {
        CreateState();
        
        stateMachine.Initialize(idleState);

        GetCoreComponent();
    }

    void CreateState()
    {
        stateMachine = new StateMachine();
        
        idleState = new IdleState(this, core, stateMachine, data, idleId);
        inAirState = new InAirState(this, core, stateMachine, data, inAirId);
        jumpState = new JumpState(this, core, stateMachine, data, jumpId);
        dashState = new DashState(this, core, stateMachine, data, dashId);
        hitState = new HitState(this, core, stateMachine, data, hitId);
        moveState = new MoveState(this, core, stateMachine, data, moveId);
        meleeAttackState = new MeleeAttackState(this, core, stateMachine, data, meleeAttackId);
    }

    void GetCoreComponent()
    {
        SetUpCombatComponent(core.GetCoreComponent<Combat>());
        SetUpHealthComponent(core.GetCoreComponent<Health>());
        SetUpRecoveryComponent(core.GetCoreComponent<RecoveryController>());
    }

    void SetUpHealthComponent(Health health)
    {
        health.SetHealth(data.healthData);
    }

    void SetUpCombatComponent(Combat combat)
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Player, IDamageable.KnockbackType.weak); 
    }

    void SetUpRecoveryComponent(RecoveryController recoveryController)
    {
        recoveryController.SetHitData(data.hitData);
    }

    #endregion

    void Update() 
    {
        stateMachine.Update();
    }

    void FixedUpdate() 
    {
        stateMachine.FixedUpdate();
    }

    #region Get 

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        return core.GetCoreComponent<T>();
    }

    #endregion

    #region On Draw Gizmos
    
    
    private void OnDrawGizmosSelected() 
    {
        if (data.meleeAttackData == null) return;
        
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(data.meleeAttackData.leftAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
        Gizmos.DrawWireSphere(data.meleeAttackData.rightAttackPos + (Vector2)transform.position, data.meleeAttackData.radius);    
    }


    #endregion

    private void OnTriggerStay2D(Collider2D other) {
        if (stateMachine.currentState == dashState && other.TryGetComponent<Acid>(out Acid acid))
        {
            acid.Release();
        }
    }

}
