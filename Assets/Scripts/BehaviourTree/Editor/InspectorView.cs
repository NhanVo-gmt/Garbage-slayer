using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>{}
    Editor editor;
    [NonSerialized] Node selectedNode;

    public InspectorView()
    {
        
    }

    public void UpdateSelection(NodeView nodeView)
    {
        Clear();
        
        selectedNode = nodeView.node;

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(nodeView.node);
        IMGUIContainer iMGUIContainer = new IMGUIContainer(() => {
            if (editor.target)
            {
                editor.OnInspectorGUI();
            }
        });
        Add(iMGUIContainer);

        
    }

    public void DrawGizmos() 
    {
        if (selectedNode != null)
        {
            selectedNode.DrawGizmos();
        }
    }
}
