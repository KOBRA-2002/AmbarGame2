using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    private T prefab;
    private int sizeOfPool;
    private Transform placePool;

    private List<T> elementsOfPool;

    public PoolMono(T prefab, int sizeOfPool, Transform placePool)
    {
        this.prefab = prefab;
        this.sizeOfPool = sizeOfPool;
        this.placePool = placePool;

        CreateElementsOfPool();
    }

    private void CreateElementsOfPool()
    {
        //Debug.Log("Создается пул объектов");
        elementsOfPool = new List<T>();
        for(int i = 0; i < sizeOfPool; i++)
        {
            elementsOfPool.Add(CreateElement());
        }
    }

    private T CreateElement(bool isActive = false) 
    {
        var newElement = Object.Instantiate(this.prefab, placePool);
        newElement.gameObject.SetActive(isActive);
        return newElement;
    }

    private bool HasFreeElement(out T freeElement)
    {
        foreach(var element in elementsOfPool)
        {
            if (!element.gameObject.activeInHierarchy)
            {
                freeElement = element;
                freeElement.gameObject.SetActive(true);
                return true;
            }
        }

        freeElement = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var freeElement))
        {
            return freeElement;
        }
        else
            return CreateElement(true);
    }
}
