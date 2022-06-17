using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTool : MonoBehaviour
{
    void Start()
    {
        Deactivate();
    }

    /// <summary>
    /// Включить рабочий инструмент (Серп).
    /// </summary>
    public void Activate()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    /// <summary>
    /// Отключить рабочий инструмент (Серп).
    /// </summary>
    public void Deactivate()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
