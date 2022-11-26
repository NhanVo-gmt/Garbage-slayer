using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq;

public class CreatePrefabFromAnimationClip : MonoBehaviour
{

#if UNITY_EDITOR

    static string PooledObjectPath = "Assets/Game/Prefabs/PooledObject/PooledObject.prefab";
    static string EnemyObjectPath = "Assets/Game/Prefabs/Character/Enemy.prefab";
    [NonSerialized] static List<AnimationClip> clipSelected = new List<AnimationClip>();

    [MenuItem("Knight/Create empty prefab from animation clip")]
    static void CreatePooledPrefab()
    {
        OnSelectionChange();
        
        if (clipSelected.Count == 0) 
        {
            Debug.Log("You haven't selected an animation clip");
            return;
        }
        
        GameObject pooledObject = GetObjectFromPath(PooledObjectPath);
        if (pooledObject == null) return;

        string pathToCreate = GetPathFromSelection();
        GameObject prefab = CreatePrefab(pooledObject, pathToCreate);

        AddAnimatorComponentToPrefab(prefab, pathToCreate);
    }

    [MenuItem("Knight/Create empty enemy prefab from animation clip")]
    static void CreateEnemyPrefab()
    {
        OnSelectionChange();
        
        if (clipSelected.Count == 0) 
        {
            Debug.Log("You haven't selected an animation clip");
            return;
        }
        
        GameObject enemyObject = GetObjectFromPath(EnemyObjectPath);
        if (enemyObject == null) return;

        string pathToCreate = GetPathFromSelection();
        GameObject prefab = CreatePrefab(enemyObject, pathToCreate);

        AddAnimatorComponentToPrefab(prefab, pathToCreate);
    }

    private static void OnSelectionChange() {
        clipSelected.Clear();
        foreach(AnimationClip clip in Selection.GetFiltered(typeof(AnimationClip), SelectionMode.Editable))
        {
            clipSelected.Add(clip);
        }
    }

    static GameObject GetObjectFromPath(string path)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        if (prefab == null)
        {
            Debug.Log("There is no prefab at " + path);
        }
        else
        {
            return Instantiate(prefab);
        }
        return null;
    }

    static GameObject GetAnimControllerFromEnemyPrefabPath()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath(PooledObjectPath, typeof(GameObject)) as GameObject;
        if (prefab == null)
        {
            Debug.Log("There is no prefab at " + PooledObjectPath);
        }
        else
        {
            return Instantiate(prefab);
        }
        return null;
    }

    static string GetPathFromSelection()
    {
        return Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeInstanceID));
    }

    static GameObject CreatePrefab(GameObject pooledPrefab, string path)
    {
        string createdPath = path + "/" + CreatePrefabName() + ".prefab";

        createdPath = AssetDatabase.GenerateUniqueAssetPath(createdPath);

        bool prefabSuccess;
        //GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(pooledPrefab, createdPath, InteractionMode.UserAction, out prefabSuccess);
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(pooledPrefab, createdPath, out prefabSuccess);
        if (prefabSuccess)
        {
            Debug.Log("Successfully created a prefab");

            return prefab;
        }
        else
        {
            Debug.LogError("Failed to create a prefab");

            return null;
        }
    }

    static string CreatePrefabName()
    {
        for (int i = 0; i < clipSelected[0].name.Length; i++)
        {
            if (clipSelected[0].name[i] == '_')
            {
                return clipSelected[0].name.Substring(0, i);
            }
        }

        return clipSelected[0].name;
    }

    static void AddAnimatorComponentToPrefab(GameObject prefab, string path)
    {
        Animator anim = prefab.GetComponent<Animator>();

        if (anim == null)
        {
            anim = prefab.GetComponentInChildren<Animator>();
        }

        anim.runtimeAnimatorController = CreateAnimatorComponent(path);
    }

    static UnityEditor.Animations.AnimatorController CreateAnimatorComponent(string path)
    {
        string createdPath = path + "/" + GetAnimatorName(clipSelected[0].name) + ".controller";
        var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(createdPath);
        AddAnimationClipToAnimator(controller);

        return controller;
    }

    

    static string GetAnimatorName(string clipName)
    {
        string name = "";
        for(int i = 0; i < clipName.Length; i++)
        {
            if (clipName[i] == '_') 
            {
                return name;
            }
            name += clipName[i];
        }
        
        return name;
    }

    static void AddAnimationClipToAnimator(UnityEditor.Animations.AnimatorController controller)
    {
        foreach(AnimationClip clip in clipSelected)
        {
            UnityEditor.Animations.AnimatorState animatorState = controller.AddMotion(clip);
            animatorState.name = CreateAnimationClipName(clip);
        }
    }

    static string CreateAnimationClipName(AnimationClip clip)
    {
        for (int i = 0; i < clip.name.Length; i++)
        {
            if (clip.name[i] == '_')
            {
                return clip.name.Substring(i + 1);
            }
        }

        return clip.name;
    }

#endif
}
