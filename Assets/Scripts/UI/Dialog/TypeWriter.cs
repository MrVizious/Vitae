using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private IEnumerator typingCoroutine = null;
    private string textToType;

    private void Awake() {

        text = GetComponent<TextMeshProUGUI>();
    }
    private void Start() {
        if (text.text != null && text.text != "")
        {
            TypeText(text.text, 0.07f);
        }
    }
    public void TypeText(string newTextToType, float secondsPerCharacter) {
        if (typingCoroutine == null)
        {
            textToType = newTextToType;
            typingCoroutine = TypingCoroutine(secondsPerCharacter);
            StartCoroutine(typingCoroutine);
        }
    }

    IEnumerator TypingCoroutine(float secondsPerCharacter) {

        text.text = "";
        int numCharsRevealed = 0;

        while (numCharsRevealed < textToType.Length)
        {
            while (textToType[numCharsRevealed] == ' ')
                ++numCharsRevealed;

            ++numCharsRevealed;

            text.text = textToType.Substring(0, numCharsRevealed);

            yield return new WaitForSecondsRealtime(secondsPerCharacter);
        }
        typingCoroutine = null;
    }

    public void ShowCompleteText() {
        if (isTyping())
        {
            StopCoroutine(typingCoroutine);
            text.text = textToType;
            typingCoroutine = null;
        }
    }

    public bool isTyping() {
        return typingCoroutine != null;
    }
}
