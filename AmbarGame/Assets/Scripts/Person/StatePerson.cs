using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePerson : State
{
    private Joystick joystick;
    private CanvasController canvasController;
    /// <summary>
    /// Данные с джойстика.
    /// </summary>
    protected Vector3 Dir { private set; get; }
    /// <summary>
    /// Статус кнопки "Косить".
    /// </summary>
    //protected bool IsMow { private set; get; }

    protected SMPerson smPerson;
    protected PersonController personController;

    protected StatePerson(SMPerson smPerson, PersonController personController)
    {
        this.smPerson = smPerson;
        this.personController = personController;

        joystick = ServiceLocator.instance.Joystick;
        canvasController = ServiceLocator.instance.CanvasController;
        canvasController.StartListenMowButton(() => smPerson.SetMowState());
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void LogicUpdate()
    {
        this.Dir = new Vector3(joystick.Dir.x, 0, joystick.Dir.y);
    }

    public override void Update()
    {

    }
}
