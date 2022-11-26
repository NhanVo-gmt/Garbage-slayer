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
    SpawnObjectController vfx;
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
    }

    private void GetCoreComponent()
    {
        combat = core.GetCoreComponent<Combat>();
        movement = core.GetCoreComponent<Movement>();
        health = core.GetCoreComponent<Health>();
        vfx = core.GetCoreComponent<SpawnObjectController>();

        SetUpComponent();
    }

    void SetUpComponent()
    {
        combat.SetUpCombatComponent(IDamageable.DamagerTarget.Enemy, data.knockbackType);
        health.SetHealth(data.healthData);
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == col) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(1, IDamageable.DamagerTarget.Enemy, Vector2.zero); //hardcode
        }
    }
}
