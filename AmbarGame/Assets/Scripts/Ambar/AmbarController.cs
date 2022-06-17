using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbarController : MonoBehaviour
{
    private CanvasController canvasController;
    void Start()
    {
        canvasController = ServiceLocator.instance.CanvasController;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Игрок подошел к Амбару");
            canvasController.ActiveSellButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Игрок отошел от Амбара");
            canvasController.DeactiveSellButton();
        }
    }
}
