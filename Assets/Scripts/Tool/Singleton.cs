using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    
    //Can not Override Awake() in classes extends from Singleton
    protected virtual void Awake() {
        if (Instance == null)
            Instance = this as T;
    }
}
