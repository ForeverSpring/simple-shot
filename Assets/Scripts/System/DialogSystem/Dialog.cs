using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Dialog", fileName = "newDialog")]
[System.Serializable]
public class Dialog : ScriptableObject {
    [SerializeField] public List<DialogSentence> sentences;
}
[System.Serializable]
public class DialogSentence {
    [SerializeField] public string logger;
    [TextArea] public string content;
    public DialogSentence(string logger, string content) {
        this.logger = logger;
        this.content = content;
    }
}
