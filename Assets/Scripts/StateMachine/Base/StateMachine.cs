using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    IState currentState;
    protected Dictionary<System.Type, IState> statetable;
    private void Update() {
        currentState.LogicUpdate();
    }

    private void FixedUpdate() {
        currentState.PhysicUpdate();
    }

    public void SwitchOn(IState newState) {
        currentState = newState;
        currentState.Enter();
    }

    public void SwitchState(IState newState) {
        currentState.Exit();
        SwitchOn(newState);
    }

    public void SwitchState(System.Type newStateType) {
        SwitchState(statetable[newStateType]);
    }

}
