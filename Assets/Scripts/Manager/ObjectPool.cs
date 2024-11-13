using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public Transform poolContainer;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        if (poolContainer == null)
        {
            poolContainer = transform;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, poolContainer);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab, poolContainer);
        }

        obj.SetActive(true);
        obj.transform.SetParent(poolContainer);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(poolContainer);
        pool.Enqueue(obj);
    }
}
