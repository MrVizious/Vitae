using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1)]
public class DialogScriptableObject : ScriptableObject
{
    public List<Sentence> sentences;
    private int currentSentenceIndex;

    private void Start() {
        currentSentenceIndex = 0;
    }

    public Sentence GetCurrentSentence() {
        Sentence returnSentence = sentences[currentSentenceIndex];
        currentSentenceIndex++;
        return returnSentence;
    }
}

[System.Serializable]
public struct Sentence
{
    public Character character;
    public string text;

}
