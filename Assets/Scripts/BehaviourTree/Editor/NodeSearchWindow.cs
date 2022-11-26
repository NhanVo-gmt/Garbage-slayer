using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    BehaviourTreeGraphView graphView;
    EditorWindow window;

    public void Initialize (EditorWindow window, BehaviourTreeGraphView graphView)
    {
        this.window = window;
        this.graphView = graphView;
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node"), level: 0)
        };

        AddSearchEntryForEachType(tree);
    
        return tree;
    }

    void AddSearchEntryForEachType(List<SearchTreeEntry> tree)
    {
        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Action Node"), level: 1));

        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (System.Type type in types)
            {
                tree.Add(CreateNodeFromType(type));
            }
        }

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Composite Node"), level: 1));

        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (System.Type type in types)
            {
                tree.Add(CreateNodeFromType(type));
            }
        }

        tree.Add(new SearchTreeGroupEntry(new GUIContent("Create Decorator Node"), level: 1));

        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (System.Type type in types)
            {
                tree.Add(CreateNodeFromType(type));
            }
        }
    }

    SearchTreeEntry CreateNodeFromType(System.Type type)
    {
        return new SearchTreeEntry(new GUIContent(type.Name))
        {
            userData = type,
            level = 2
        };
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = graphView.ChangeCoordinatesTo(graphView.parent, context.screenMousePosition - window.position.position);
        var localMousePosition = graphView.contentContainer.WorldToLocal(worldMousePosition);
        

        if (SearchTreeEntry.userData is System.Type)
        {
            graphView.CreateNode(SearchTreeEntry.userData as System.Type, localMousePosition);
            return true;
        }

        return false;
    }
}
