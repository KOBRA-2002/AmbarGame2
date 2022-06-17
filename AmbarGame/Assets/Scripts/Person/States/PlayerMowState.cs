using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMowState : StatePerson
{
    public PlayerMowState(SMPerson smPerson, PersonController personController) :
        base(smPerson, personController)
    {

    }

    public override void Enter()
    {
        Debug.Log("Вход в состояние сбора пшеницы");
        personController.PlayMowClip();
    }

    public override void Exit()
    {
        personController.DeactivateWorkTool();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (personController.IsEndStatusForMowClip)
        {
            smPerson.SetIdleState();
            personController.IsEndStatusForMowClip = false;
        }
    }

    public override void Update()
    {
        base.Update();
        if (personController.IsMomentWorm)
        {
            personController.ActivateWorkTool();
            personController.IsMomentWorm = false;
        }
    }
}
