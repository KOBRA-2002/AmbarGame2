using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float maxDelta = 10f;

    [SerializeField] private Image Handler;
    [SerializeField] private Image BaseJoystick;

    /// <summary>
    /// Инверсия управления.
    /// </summary>
    [SerializeField] private bool isInverse = true;  

    private Vector2 startCoordinateOfHandler;
    private Vector2 startCoordinateOfBaseJoystick;

    public Vector2 Dir { private set; get; }

    void Start()
    {
        startCoordinateOfHandler = Handler.transform.position;
        startCoordinateOfBaseJoystick = Handler.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var offset = eventData.position - (Vector2)BaseJoystick.transform.position;// startCoordinateOfHandler;
        offset = Vector2.ClampMagnitude(offset, maxDelta);
        Handler.transform.position = (Vector2)BaseJoystick.transform.position + offset;
        Dir = offset / maxDelta;
        if (isInverse)
            Dir = -Dir;
        //Debug.Log(Dir);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Пользователь коснулся экрана : " + eventData.position);
        BaseJoystick.transform.position = eventData.position;
        Handler.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Пользователь отпустил джостик");
        BaseJoystick.transform.position = startCoordinateOfBaseJoystick;
        Handler.transform.position = startCoordinateOfHandler;
        Dir = Vector2.zero;
    }
}
