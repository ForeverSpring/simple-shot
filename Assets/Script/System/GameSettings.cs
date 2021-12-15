using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    //player settings
    [SerializeField] public float playerBulletSpeed = 8f;
    [SerializeField] public float playerMoveSpeedHigh = 5.8f;
    [SerializeField] public float playerMoveSpeedLow = 3.1f;
    [SerializeField] public float playerFireRate = 0.2f;
    [SerializeField] public int playerStartLifeNum = 5;
    [SerializeField] public int playerStartBombNum = 3;
    //danmu settings
    [SerializeField] public int danmuReflectTimes = 5;
    [SerializeField] public float danmuMoveSpeed = 5;
    [SerializeField] public float danmuRotateSpeed = 0.5f;
}
