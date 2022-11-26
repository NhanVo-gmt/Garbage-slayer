using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data; // todo set private (for unity editor to see)
    public BehaviourTree tree;

    Core core;
    Combat combat;
    Movement movement;
    Health health;
    SpawnObjectController spawnObjectController;
    PooledObject pooledObject;
    public BehaviourTreeComponent treeComponent {get; private set;}

    Collider2D col;

    #region Set up
    
    void Awake() 
    {
        col = GetComponent<Collider2D>();
        core = GetComponentInChildren<Core>();
        
        data = Instantiate(data);

        SetUpBehaviourTree();
    }

    void SetUpBehaviourTree()
    {
        InitializeTreeComponent();

        CloneTree();
    }

    void InitializeTreeComponent() 
    {
        treeComponent = BehaviourTreeComponent.CreateTreeComponentFromGameObject(gameObject, data);
    }

    void CloneTree() 
    {
        tree = tree.Clone();
    }

    void Start() 
    {
        GetCoreComponent();

        InitializeTreeNodeComponent();

        pooledObject = GetComponent<PooledObject>();
    }

    private void OnEnable() {
        if (health == null) return;

        health.onDie += Die;
    }

    private void OnDisable() {
        health.onDie -= Die;
    }

    private void GetCoreComponent()
    {
        combat = core.GetCoreComponent<Combat>();
        movement = core.GetCoreComponent<Movement>();
        health = core.GetCoreComponent<Health>();
        spawnObjectController = core.GetCoreComponent<SpawnObjectController>();

        SetUpComponent();
    }

    void SetUpComponent()
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Enemy, data.knockbackType);
        health.SetHealth(data.healthData);
        health.onDie += Die;
    }

    void InitializeTreeNodeComponent()
    {
        Player player = FindObjectOfType<Player>();
        
        tree.Traverse(tree.rootNode, (n) =>
        {
            n.treeComponent = treeComponent;
            n.player = player;
        });
    }

    #endregion

    #region Unity Call back

    void Update() 
    {
        tree.Update();
    }

    #endregion

    private void OnTriggerStay2D(Collider2D other) {
        if (other == col) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, Vector2.zero);
            
            if (data.isBoss) return;
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!data.isFalling) return;

        if (other.collider.CompareTag("Ground"))
        {
            Die();

            if (data.isToxic)
            {
                SpawnToxic();
            }
        }
    }

    private void SpawnToxic()
    {
        Vector2 spawnPos = new Vector2(transform.position.x, -4.2f);
        spawnObjectController.SpawnGameObject(data.toxic, spawnPos);
    }

    void Die() 
    {
        if (pooledObject != null)
        {
            pooledObject.Release();
        }
        
        tree.Reset();
    }


}
