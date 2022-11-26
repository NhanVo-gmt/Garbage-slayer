using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using System.Linq;

public class BehaviourTreeGraphView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeGraphView, GraphView.UxmlTraits>{}
    public Action<NodeView> onNodeViewSelectionChanged;
    BehaviourTree tree;
    BehaviourTreeSettings settings;

    EditorWindow editorWindow;
    NodeSearchWindow searchWindow;

    [NonSerialized] List<Node> nodeCopiedList = new List<Node>();

    public struct ScriptTemplate
    {
        public TextAsset templateFile;
        public string templateFileName;
        public string subFolder;
    }

    public ScriptTemplate[] scriptTemplateAsset =
    {
        new ScriptTemplate{templateFile = BehaviourTreeSettings.GetOrCreateSettings().ActionNodeScriptTemplate, templateFileName = "NewActionNode.cs", subFolder = "ActionNode/"},
        new ScriptTemplate{templateFile = BehaviourTreeSettings.GetOrCreateSettings().CompositeNodeScriptTemplate, templateFileName = "NewCompositeNode.cs", subFolder = "CompositeNode/"},
        new ScriptTemplate{templateFile = BehaviourTreeSettings.GetOrCreateSettings().DecoratorNodeScriptTemplate, templateFileName = "NewDecoratorNode.cs", subFolder = "DecoratorNode/"},
    };


    public BehaviourTreeGraphView()
    {
        settings = BehaviourTreeSettings.GetOrCreateSettings();
        
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
        
        serializeGraphElements += CutCopyOperation;
        canPasteSerializedData += CanPaste;
        unserializeAndPaste += PasteOperation;

        AddSearchWindow();
    }

    private void AddSearchWindow()
    {
        searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
    }

    void InitializeSearchWindow(EditorWindow window)
    {
        searchWindow.Initialize(window, this);
    }

    private void OnUndoRedo()
    {
        PopulateView(tree, editorWindow);
        AssetDatabase.SaveAssets();
    }

    public void PopulateView(BehaviourTree tree, EditorWindow editorWindow)
    {
        this.tree = tree;
        this.editorWindow = editorWindow;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        CreateRootNode();

        tree.nodes.ForEach(n => CreateNodeView(n));
        tree.nodes.ForEach(n => CreateEdge(n));

        InitializeSearchWindow(editorWindow);
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            foreach(GraphElement ele in graphViewChange.elementsToRemove)
            {
                NodeView nodeView = ele as NodeView;
                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                }

                Edge edge = ele as Edge;
                if (edge != null)
                {
                    NodeView inputNodeView = edge.input.node as NodeView;
                    NodeView outputNodeView = edge.output.node as NodeView;
                    tree.RemoveChild(outputNodeView.node, inputNodeView.node);
                }
            }
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge => 
            {
                NodeView inputNodeView = edge.input.node as NodeView;
                NodeView outputNodeView = edge.output.node as NodeView;
                tree.AddChild(outputNodeView.node, inputNodeView.node);
            });
        }

        if (graphViewChange.movedElements != null)
        {
            nodes.ForEach((n) =>
            {
                NodeView nodeView = n as NodeView;
                if (nodeView != null)
                {
                    nodeView.SortChildren();
                }
            });
        }
        
        return graphViewChange;
    }

    private string CutCopyOperation(IEnumerable<GraphElement> elements)
    {
        nodeCopiedList.Clear();
        foreach(GraphElement element in elements)
        {
            NodeView nodeView = element as NodeView;
            if (nodeView != null)
            {
                nodeCopiedList.Add(nodeView.node);
            }
        }
        

        return "success";
    }

    private bool CanPaste(string data)
    {
        return true;
    }

    private void PasteOperation(string operationName, string data)
    {
        List<Node> pastedNode = tree.PasteNode(nodeCopiedList);
        pastedNode.ForEach(node => CreateNodeView(node));
        pastedNode.ForEach(node => CreateEdge(node));
    }



    void CreateRootNode()
    {
        if (tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
    }

    

    private void CreateNodeView(Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.onSelectedChanged = onNodeViewSelectionChanged;
        AddElement(nodeView);
    }

    private void CreateEdge(Node parent)
    {
        NodeView parentNodeView = FindNodeView(parent);
        
        foreach(Node childNode in tree.GetAllChild(parent))
        {
            NodeView childNodeView = FindNodeView(childNode);
            Edge edge = parentNodeView.output.ConnectTo(childNodeView.input);
            AddElement(edge);
        }
    }

    NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => 
        endPort.direction != startPort.direction && 
        endPort.node != startPort.node).ToList();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        evt.menu.AppendAction($"Create New Node Scripts.../Action Node Scripts", (a) => CreateNodeScript(scriptTemplateAsset[0]));
        evt.menu.AppendAction($"Create New Node Scripts.../Composite Node Scripts", (a) => CreateNodeScript(scriptTemplateAsset[1]));
        evt.menu.AppendAction($"Create New Node Scripts.../Decorator Node Scripts", (a) => CreateNodeScript(scriptTemplateAsset[2]));

        // {
        //     var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
        //     foreach (var type in types)
        //     {
        //         evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", (a) => CreateNode(type));
        //     }
        // }

        // {
        //     var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
        //     foreach (var type in types)
        //     {
        //         evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", (a) => CreateNode(type));
        //     }
        // }

        // {
        //     var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
        //     foreach (var type in types)
        //     {
        //         evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", (a) => CreateNode(type));
        //     }
        // }
    }

    void CreateNodeScript(ScriptTemplate scriptTemplate)
    {
        SelectFolder($"{settings.newNodeBasePath}");
        string templatePath = AssetDatabase.GetAssetPath(scriptTemplate.templateFile);
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, scriptTemplate.templateFileName);
    }

    void SelectFolder(string path) {
        // https://forum.unity.com/threads/selecting-a-folder-in-the-project-via-button-in-editor-window.355357/
        // Check the path has no '/' at the end, if it does remove it,
        // Obviously in this example it doesn't but it might
        // if your getting the path some other way.

        // if (path[path.Length - 1] == '/')
        //     path = path.Substring(0, path.Length - 1);

        // Load object
        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

        // Select the object in the project folder
        Selection.activeObject = obj;

        // Also flash the folder yellow to highlight it
        EditorGUIUtility.PingObject(obj);
    }

    public void CreateNode(System.Type type, Vector2 position)
    {
        Node node = tree.CreateNode(type);
        node.name = type.Name;
        node.position = position;
        
        CreateNodeView(node);
    }

    

    public void UpdateNodeState()
    {
        nodes.ForEach(n => 
        {
            NodeView nodeView = n as NodeView;
            if (nodeView != null)
            {
                nodeView.UpdateState();
            }
        });
    }
}
