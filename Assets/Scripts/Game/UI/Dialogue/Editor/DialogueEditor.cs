using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

public class DialogueEditor : EditorWindow {

    [NonSerialized] Dialogue selectedDialogue;
    [NonSerialized] GUIStyle nodeStyle;
    [NonSerialized] GUIStyle rootNodeStyle;

    [NonSerialized] DialogueNode draggingNode;
    [NonSerialized] DialogueNode creatingNode;
    [NonSerialized] DialogueNode deletingNode;
    [NonSerialized] DialogueNode linkingNode;

    Vector2 draggingOffset;
    Vector2 scrollPosition;
    float canvasSize = 4000;



    [MenuItem("Knight/Dialogue")]
    private static void ShowWindow() {
        GetWindow(typeof(DialogueEditor), false, "Dialogue");
    }

    [OnOpenAssetAttribute(1)]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceId) as Dialogue;
        if (dialogue != null)
        {
            ShowWindow();
        }

        return false;
    }

    void OnEnable() 
    {
        Selection.selectionChanged += OnChangeDialogue;

        SetGUIStyle();
    }

    void SetGUIStyle()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        
        int borderOffset = 10;
        nodeStyle.border = new RectOffset(borderOffset, borderOffset, borderOffset, borderOffset);

        int paddingOffset = 10;
        nodeStyle.padding = new RectOffset(paddingOffset, paddingOffset, paddingOffset, paddingOffset);

        rootNodeStyle = new GUIStyle();
        rootNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;

        rootNodeStyle.border = new RectOffset(borderOffset, borderOffset, borderOffset, borderOffset);
        rootNodeStyle.padding = new RectOffset(paddingOffset, paddingOffset, paddingOffset, paddingOffset);
    }

    void OnChangeDialogue()
    {
        Dialogue newDialogue = Selection.activeObject as Dialogue;
        if (newDialogue != null)
        {
            selectedDialogue = newDialogue;
            Repaint();
        }
    }

    private void OnGUI() {
        if (selectedDialogue != null)
        {
            ProcessEvent();
            OnDialogueGUI();
            ProcessNodeEvent();
        }
    }


    void ProcessEvent()
    {
        if (Event.current.type == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodeAtPosition(Event.current.mousePosition + scrollPosition);
            if (draggingNode != null)
            {
                draggingOffset = draggingNode.rect.position - Event.current.mousePosition - scrollPosition;
                Selection.activeObject = draggingNode;
            }
            else
            {
                Selection.activeObject = selectedDialogue; 
            }
        }
        else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
        {
            draggingNode.rect.position = Event.current.mousePosition + draggingOffset + scrollPosition;
            GUI.changed = true;
        }
        else if (Event.current.type == EventType.MouseUp && draggingNode != null)
        {
            draggingNode = null;
        }
    }

    private DialogueNode GetNodeAtPosition(Vector2 mousePosition)
    {
        DialogueNode currentNode = null;
        
        foreach(DialogueNode node in selectedDialogue.GetListNode())
        {
            if (node.rect.Contains(mousePosition))
            {
                currentNode = node;
            }
        }

        return currentNode;
    }

    void OnDialogueGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayoutUtility.GetRect(canvasSize, canvasSize);

        foreach(DialogueNode node in selectedDialogue.GetListNode())
        {
            DrawNode(node);
            DrawConnection(node);
        }

        GUILayout.EndScrollView();
    }

    private void DrawNode(DialogueNode node)
    {
        GUILayout.BeginArea(node.rect, nodeStyle);
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.LabelField("Node:");

        string newText = EditorGUILayout.TextArea(node.text, 
                                                GUILayout.MinHeight(40),
                                                GUILayout.MaxHeight(200));

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(selectedDialogue, "Undo dialogue text");
            node.text = newText;
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("+"))
        {
            creatingNode = node;
        }

        if (linkingNode != null)
        {
            if (linkingNode == node)
            {
                if (GUILayout.Button("UnChild"))
                {
                    linkingNode = null;
                }
            }
            else if (node == selectedDialogue.GetRootNode())
            {
                GUILayout.Button("Can not link");
            }
            else if (linkingNode.HaveChildNode(node.name) )
            {   
                if (GUILayout.Button("Unlink"))
                {
                    linkingNode.RemoveChildNode(node.name);
                }
            }
            else if (node.HaveChildNode(linkingNode.name))
            {
                if (GUILayout.Button("Unlink"))
                {
                    node.RemoveChildNode(linkingNode.name);
                }
            }
            else
            {
                if (GUILayout.Button("Link"))
                {
                    linkingNode.AddChildNode(node.name);
                }
            }
        }
        else 
        {
            if (GUILayout.Button("Child"))
            {
                linkingNode = node;
            }
        }

        if (GUILayout.Button("-"))
        {
            deletingNode = node;
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void ProcessNodeEvent()
    {
        if (creatingNode != null)
        {
            selectedDialogue.MakeNode(creatingNode);
            creatingNode = null;
        }

        if (deletingNode != null)
        {
            selectedDialogue.DeletingNode(deletingNode);
            deletingNode = null;
        }
    }

    void DrawConnection(DialogueNode node)
    {
        foreach(DialogueNode child in selectedDialogue.GetAllChildren(node))
        {
            Vector3 startPos = new Vector2(node.rect.xMax, node.rect.center.y);
            Vector3 endPos = new Vector2(child.rect.xMin, child.rect.center.y);
            Handles.DrawBezier(startPos, endPos, startPos, endPos, Color.white, null, 5f);
        }
    }
}

