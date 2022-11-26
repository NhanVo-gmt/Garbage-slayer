using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DialogueNode : ScriptableObject
{
    [TextArea(5, 10)] public string text;
    [SerializeField] List<string> childList = new List<string>();
    public Rect rect = new Rect(10, 10, 200, 150);

    public IEnumerable<string> GetChildList()
    {
        return childList;
    }

    public bool HaveChildNode(string nodeName)
    {
        return childList.Contains(nodeName);
    }

    public string GetNextNode()
    {
        if (childList.Count > 0) return childList[0];
        return "";
    }

#if UNITY_EDITOR

    public void AddChildNode(string nodeName)
    {
        Undo.RecordObject(this, "Add new child node");
        if (childList.Contains(nodeName)) return;
        childList.Add(nodeName);
    }

    public void RemoveChildNode(string nodeName)
    {
        Undo.RecordObject(this, "Remove child node");
        if (!childList.Contains(nodeName)) return;
        childList.Remove(nodeName);
    }

#endif
}
