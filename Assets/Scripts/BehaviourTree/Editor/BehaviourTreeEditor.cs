using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
using System;

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeGraphView treeGraphView;
    InspectorView inspectorView;
    [NonSerialized] NodeView selectedNodeView;
    
    [MenuItem("Knight/BehaviourTreeEditor")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    [OnOpenAssetAttribute(0)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        BehaviourTree tree = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;
        if (tree != null)
        {
            OpenWindow();
            return true;
        }

        return false;
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;


        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");

        root.styleSheets.Add(styleSheet);

        treeGraphView = root.Q<BehaviourTreeGraphView>();
        inspectorView = root.Q<InspectorView>();
        treeGraphView.onNodeViewSelectionChanged = OnNodeSelectionChange;

        OnSelectionChange();
    }

    private void OnEnable() {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
        EditorApplication.playModeStateChanged += OnPlayModeStateChange;
    }

    private void OnDisable() {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
    }

    private void OnPlayModeStateChange(PlayModeStateChange obj)
    {
        switch(obj)
        {
            case PlayModeStateChange.EnteredEditMode:
            {
                OnSelectionChange();
                break;
            }
            case PlayModeStateChange.EnteredPlayMode:
            {
                OnSelectionChange();
                break;
            }
        }
    }


    private void OnSelectionChange() {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (!tree)
        {
            if (Selection.activeGameObject)
            {
                BehaviourTreeRunner behaviourTreeRunner = Selection.activeGameObject.GetComponent<BehaviourTreeRunner>();
                if (behaviourTreeRunner)
                {
                    tree = behaviourTreeRunner.tree;
                }

                //todo
                // Enemy enemy = Selection.activeGameObject.GetComponent<Enemy>();
                // if (enemy)
                // {
                //     tree = enemy.tree;
                // }
            }
        }

        if (treeGraphView != null)
        {
            if (Application.isPlaying)
            {
                if (tree != null)
                {
                    treeGraphView.PopulateView(tree, this);
                }
            }
            else
            {
                if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                {
                    treeGraphView.PopulateView(tree, this);
                }
            }
        }
        
    }

    void OnNodeSelectionChange(NodeView nodeView)
    {
        inspectorView.UpdateSelection(nodeView);
        selectedNodeView = nodeView;
    }

    private void OnInspectorUpdate() {
        treeGraphView?.UpdateNodeState();
    }

    private void Update() {
        if (!Application.isPlaying)
        {
            inspectorView.DrawGizmos();

            if (selectedNodeView != null)
            {
                selectedNodeView.UpdateDescription();
            }
        }
    }
}