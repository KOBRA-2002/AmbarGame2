using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private PersonController personController;
    [SerializeField] private Pool pool;

    public static ServiceLocator instance { private set; get; }
    /// <summary>
    /// Ссылка на джойстик.
    /// </summary>
    public Joystick Joystick => joystick;
    /// <summary>
    /// Ссылка на канвас.
    /// </summary>
    public CanvasController CanvasController => canvasController;
    /// <summary>
    /// Ссылка на канвас.
    /// </summary>
    public Pool Pool => pool;
    /// <summary>
    /// Ссылка на персонажа.
    /// </summary>
    public PersonController PersonController => personController;

    private void Awake()
    {
        instance = this;
        DOTween.Init();
    }

    private void InitAllService()
    {

    }
}
