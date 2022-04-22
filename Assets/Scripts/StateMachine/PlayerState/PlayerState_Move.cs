using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Move", fileName = "PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    [SerializeField] float moveSpeed= 5.8f;
    [SerializeField] float moveSpeedLow = 3.1f;
    public override void Enter() {
        animator.Play("Player_MoveHorizontal");
    }
    public override void LogicUpdate() {
        if (!input.Move) {
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        //TODO:等待优化
        if (!input.MoveX) {
            animator.Play("Player_idle");
        }
        else {
            animator.Play("Player_MoveHorizontal");
        }
    }
    public override void PhysicUpdate() {
        float curSpeed = moveSpeed;
        if (input.inLowSpeed) {
            curSpeed = moveSpeedLow;
        }
        player.Move(curSpeed);

    }
}
