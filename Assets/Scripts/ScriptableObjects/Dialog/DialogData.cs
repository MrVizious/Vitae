using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1)]
public class DialogData : ScriptableObject
{
    public List<Sentence> sentences;
    public int currentSentenceIndex;

    private void OnEnable() {
        currentSentenceIndex = 0;
    }

    public Sentence GetCurrentSentence() {
        if (sentences.Count <= 0)
        {
            Debug.LogError("No sentences!");
            return null;
        }
        if (currentSentenceIndex >= sentences.Count)
        {
            return null;
        }

        Sentence returnSentence = sentences[currentSentenceIndex];
        currentSentenceIndex++;
        return returnSentence;
    }
}

[System.Serializable]
public class Sentence
{
    public Character character;
    public string text;
    public float secondsPerCharacter;

}
