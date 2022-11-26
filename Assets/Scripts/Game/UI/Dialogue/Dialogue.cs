using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject//ISerializationCallbackReceiver
{
    [SerializeField] List<DialogueNode> nodeList = new List<DialogueNode>();
    [SerializeField] DialogueNode rootNode;

    Vector2 spawnOffset = new Vector2(250, 0);

    void AddNode(DialogueNode node) 
    {
        nodeList.Add(node);
    }

    public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parent)
    {
        foreach(DialogueNode node in nodeList)
        {
            if (parent.HaveChildNode(node.name))
            {
                yield return node;
            }
        }
    }

    public DialogueNode GetRootNode()
    {
        return rootNode;
    }

    public List<DialogueNode> GetListNode()
    {
        return nodeList;
    }

    public DialogueNode GetNodeFromString(string nodeName) 
    {
        foreach(DialogueNode node in nodeList)
        {
            if (node.name == nodeName)
            {
                return node;
            }
        }

        return null;
    }

#if UNITY_EDITOR
    private void Awake() {
        if (nodeList.Count == 0)
        {
            rootNode = CreateNode();
            AddNode(rootNode);
        }

    }

    
    DialogueNode CreateNode()
    {
        DialogueNode node = CreateInstance<DialogueNode>();
        node.name = Guid.NewGuid().ToString();
        return node;
    }

    public void MakeNode(DialogueNode parent) 
    {
        DialogueNode createNode = CreateNode();

        Undo.RegisterCreatedObjectUndo(createNode, "Undo create new dialogue");
        Undo.RecordObject(this, "Undo add new object to list");

        AddNode(createNode);
        parent.AddChildNode(createNode.name);
        

        createNode.rect.position = parent.rect.position + spawnOffset;

        //OnValidate();
    }


    public void DeletingNode(DialogueNode nodeToDelete) 
    {
        if (nodeToDelete == rootNode) return;
        
        Undo.RecordObject(this, "Undo remove node from list");
        
        foreach(DialogueNode node in nodeList)
        {
            if (node == nodeToDelete) continue;

            if (node.HaveChildNode(nodeToDelete.name))
            {
                node.RemoveChildNode(nodeToDelete.name);
                
            }
        }
        nodeList.Remove(nodeToDelete);
        AssetDatabase.RemoveObjectFromAsset(nodeToDelete);
        AssetDatabase.SaveAssets();

        Undo.DestroyObjectImmediate(nodeToDelete);

    }

    //todo
    // public void OnBeforeSerialize()
    // {
    //     OnValidate();
    // }

    // void OnValidate()
    // {
    //     string path = AssetDatabase.GetAssetPath(this);
    //     if (path != "")
    //     {
    //         foreach(DialogueNode node in nodeList)
    //         {
    //             if (AssetDatabase.GetAssetPath(node) == "") 
    //             {
    //                 AssetDatabase.AddObjectToAsset(node, path);
    //                 AssetDatabase.SaveAssets();
    //             }
    //         }
    //     }
    // }

    // public void OnAfterDeserialize()
    // {

    // }


#endif
}
