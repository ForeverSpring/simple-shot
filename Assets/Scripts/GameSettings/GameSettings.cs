using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    [SerializeField] public GameSettingsData settings;
    private void Start() {
       
    }
}
