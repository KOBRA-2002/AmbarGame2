using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : StatePerson
{
    public PlayerIdleState(SMPerson smPerson, PersonController personController):
        base(smPerson, personController)
    {

    }

    public override void Enter()
    {
        //Debug.Log("Вход в состояние покоя");
        personController.PlayIdleClip();
    }

    public override void Exit()
    {
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Dir != Vector3.zero)
            smPerson.SetWalkState();
    }

    public override void Update()
    {
        base.Update();
    }
}
