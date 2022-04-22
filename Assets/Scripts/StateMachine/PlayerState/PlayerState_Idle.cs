using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState {
    public override void Enter() {
        animator.Play("Player_idle");
        player.SetVelocity(new Vector2(0, 0));
    }

    public override void LogicUpdate() {
        if (input.Move) {
            stateMachine.SwitchState(typeof(PlayerState_Move));
        }
    }
}
