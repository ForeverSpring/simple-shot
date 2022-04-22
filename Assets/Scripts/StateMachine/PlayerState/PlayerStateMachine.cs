using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states;
    Animator animator;
    PlayerController player;
    PlayerInput input;
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        player = GetComponent<PlayerController>();
        statetable = new Dictionary<System.Type, IState>(states.Length);
        foreach(PlayerState state in states) {
            state.Initialize(animator, player, input, this);
            statetable.Add(state.GetType(), state);
        }
    }

    private void Start() {
        SwitchOn(statetable[typeof(PlayerState_Idle)]);
    }
}
