using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

class BehaviourTreeSettings : ScriptableObject
{
    public VisualElement BehaviourTreeXml;
    public StyleSheet BehaviourTreeStyleSheet;
    public VisualElement NodeViewXml;
    public TextAsset ActionNodeScriptTemplate;
    public TextAsset CompositeNodeScriptTemplate;
    public TextAsset DecoratorNodeScriptTemplate;
    public string newNodeBasePath = "Assets/Scripts/BehaviourTree/Node";
    
    public static BehaviourTreeSettings GetOrCreateSettings()
    {
        string[] guids = AssetDatabase.FindAssets("t:BehaviourTreeSettings");
        if (guids.Length > 1)
        {
            Debug.LogWarning("Already have 1 setting");
            Debug.Log(AssetDatabase.GUIDToAssetPath(guids[0]));
        }

        switch (guids.Length)
        {
            case 1:
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<BehaviourTreeSettings>(path);
            }
            case 0:
            {
                return CreateSettings();
            }
        }

        return null;
    }   

    static BehaviourTreeSettings CreateSettings()
    {
        BehaviourTreeSettings settings = CreateInstance<BehaviourTreeSettings>();
        AssetDatabase.CreateAsset(settings, "Assets/ScriptableObjects/BehaviourTree/BehaviourTreeSettings.asset");
        AssetDatabase.SaveAssets();

        return settings;
    }
}
