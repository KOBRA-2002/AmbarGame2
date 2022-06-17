using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    /// <summary>
    /// Выполняется при входе в состояние
    /// </summary>
    public abstract void Enter();
    /// <summary>
    /// Выполняется каждый кадр, пока находимся в этом состоянии
    /// </summary>
    public abstract void Update();
    /// <summary>
    /// Выполняется каждый кадр, пока находимся в этом состоянии
    /// </summary>
    public abstract void LogicUpdate();
    /// <summary>
    /// Выполняется при выходе из текущего состояния
    /// </summary>
    public abstract void Exit();

}
