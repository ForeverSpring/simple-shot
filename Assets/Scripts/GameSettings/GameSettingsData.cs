using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameSettings/GameSettingsData", fileName = "GameSettingsData")]
public class GameSettingsData : ScriptableObject {
    //游戏对象初始化参数
    //player settings
    [SerializeField] public float playerBulletSpeed = 8f;
    [SerializeField] public float playerMoveSpeedHigh = 5.8f;
    [SerializeField] public float playerMoveSpeedLow = 3.1f;
    [SerializeField] public float playerFireRate = 0.2f;
    [SerializeField] public int playerStartLifeNum = 5;
    [SerializeField] public int playerStartBombNum = 3;
    //dammu settings
    [SerializeField] public int danmuReflectTimes = 5;
    [SerializeField] public float danmuMoveSpeed = 5;
    [SerializeField] public float danmuRotateSpeed = 0.5f;
}
