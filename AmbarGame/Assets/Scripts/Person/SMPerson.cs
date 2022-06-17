using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMPerson : SM
{
    private PersonController personController;
    public SMPerson(PersonController personController)
    {
        this.personController = personController;
        statesMap = new Dictionary<System.Type, State>();
        InitializeAllState();
    }
    /// <summary>
    /// Инициализировать все возможные состояния игрока
    /// </summary>
    private void InitializeAllState()
    {
        statesMap[typeof(PlayerIdleState)] = new PlayerIdleState(this, personController);
        statesMap[typeof(PlayerWalkState)] = new PlayerWalkState(this, personController);
        statesMap[typeof(PlayerMowState)] = new PlayerMowState(this, personController);
    } 

    public void SetIdleState()
    {
        var state = GetState(typeof(PlayerIdleState));
        SetState(state);
    }

    public void SetWalkState()
    {
        var state = GetState(typeof(PlayerWalkState));
        SetState(state);
    }

    public void SetMowState()
    {
        var state = GetState(typeof(PlayerMowState));
        SetState(state);
    }
}
