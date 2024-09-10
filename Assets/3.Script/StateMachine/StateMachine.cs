using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
   BaseState currentState;
    public void Start()
    {
        currentState = GetInitialState();
        if(currentState!=null)
        {
            currentState.Enter();
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }


    protected virtual BaseState GetInitialState()
    {
        return null;
    }


}
