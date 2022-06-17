using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : StatePerson
{
    public PlayerWalkState(SMPerson smPerson, PersonController personController) :
        base(smPerson, personController)
    {

    }

    public override void Enter()
    {
        //Debug.Log("Вход в состояние хотьбы");
        personController.PlayWalkClip();
    }

    public override void Exit()
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Dir == Vector3.zero)
            smPerson.SetIdleState();
        else
        {
            personController.MoveTo(Dir);
            personController.LookAtForward(Dir);
        }
        //Debug.Log(Dir);

    }

    public override void Update()
    {
        base.Update();
    }
}
