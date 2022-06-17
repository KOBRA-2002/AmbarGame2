using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Машина состояний
/// </summary>
public abstract class SM
{
    protected Dictionary<Type, State> statesMap;
    protected State currentState;

    /// <summary>
    /// Установить необходимое состояние.
    /// </summary>
    /// <param name="state">Состояние которое нужно установить.</param>
    protected void SetState(State state)
    {
        if(currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    /// <summary>
    /// Получить состояние.
    /// </summary>
    /// <param name="type">Тип состояния, которое нужно установить.</param>
    /// <returns>Возращает необходимое состояние.</returns>
    protected State GetState(Type type)
    {
        return statesMap[type];
    }

    /// <summary>
    /// Обновить состояние.
    /// </summary>
    public void UpdateState()
    {
        currentState.Update();
    }

    /// <summary>
    /// Обновить логику состояния
    /// </summary>
    public void UpdateLogicState()
    {
        currentState.LogicUpdate();
    }
}
