using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    public BehaviourTreeComponent treeComponent {get; private set;}
    

    private void Awake() {
        InitializeTreeComponent();

        CloneTree();
    }

    void Start() 
    {
        InitializeNodeComponent();
    }

    public void InitializeTreeComponent() 
    {
        treeComponent = BehaviourTreeComponent.CreateTreeComponentFromGameObject(gameObject, null);
    }

    void CloneTree() 
    {
        tree = tree.Clone();
    }

    void InitializeNodeComponent()
    {
        Player player = FindObjectOfType<Player>();
        
        tree.Traverse(tree.rootNode, (n) =>
        {
            n.treeComponent = treeComponent;
            n.player = player;
        });
    }



    void Update()
    {
        tree.Update();
    }
}
