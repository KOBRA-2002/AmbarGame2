using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    /// <summary>
    /// Стаки соломы в рюкзаке.
    /// </summary>
    [SerializeField] private GameObject[] elementsInBag; 
    void Start()
    {
        DisplayRequiredNumberOfElements(0);
    }

    /// <summary>
    /// Отобразить нужное количество элементов.
    /// </summary>
    public void DisplayRequiredNumberOfElements(int number)
    {
        for(int i = 0; i < elementsInBag.Length; i++)
        {
            if (i < number)
                elementsInBag[i].SetActive(true);
            else
                elementsInBag[i].SetActive(false);
        }
    }
}
