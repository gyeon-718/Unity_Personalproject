using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected string name;
    protected StateMachine stateMachine;

    public BaseState(string _name, StateMachine _stateMachine)
    {
        this.name = _name;
        this.stateMachine = _stateMachine;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();


}
 
 
