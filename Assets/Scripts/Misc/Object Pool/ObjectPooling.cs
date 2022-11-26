using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : SingletonObject<ObjectPooling>
{
    [System.Serializable]
    public class PooledPrefab
    {
        public GameObject prefab;
        public int amountToPool;
        public IObjectPool<GameObject> pool;

        public void OnAwake() 
        {
            pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject, false, amountToPool, amountToPool);
        }

         GameObject CreatePooledItem()
        {
            GameObject createdGO = GameObject.Instantiate(prefab);
            createdGO.AddComponent<PooledObject>().pool = pool;
            return createdGO;
        }

        void OnTakeFromPool(GameObject go)
        {
            go.gameObject.SetActive(true);
        }
        
        void OnReturnToPool(GameObject go)
        {
            go.gameObject.SetActive(false);
        }

        void OnDestroyPoolObject(GameObject go) 
        {
            GameObject.Destroy(go.gameObject);
        }
    }

    public List<PooledPrefab> pooledPrefabList = new List<PooledPrefab>();

    protected override void Awake() 
    {
        base.Awake();
        
        foreach(PooledPrefab prefab in pooledPrefabList)
        {
            prefab.OnAwake();
        }
    }

   

    public GameObject GetObjectFromPool(GameObject go)
    {
        for (int i = 0; i < pooledPrefabList.Count; i++)
        {
            if (pooledPrefabList[i].prefab == go)
            {
                return pooledPrefabList[i].pool.Get();
            }
        }

        Debug.LogError("There is no game object on this pool");
        return null;
    }
}
