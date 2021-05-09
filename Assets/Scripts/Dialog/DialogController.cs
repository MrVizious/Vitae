using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject canvas;
    public Image portrait;
    public TypeWriter text;
    public TextMeshProUGUI characterName;
    public DialogData dialog;
    public bool speaking = false;
    public UnityEvent OnDialogEnded;
    private static DialogController instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }

    }

    public static DialogController getInstance() {
        return instance;
    }

    public void Advance(InputAction.CallbackContext context) {
        if (context.started && speaking)
        {
            if (text.isTyping())
            {
                text.ShowCompleteText();
            }
            else
            {
                Sentence sentenceToShow = dialog.GetCurrentSentence();
                if (sentenceToShow == null)
                {
                    EndDialog();
                }
                else
                {
                    ShowSentence(sentenceToShow);
                }
            }
        }
    }
    public void BeginDialog(DialogData newDialog) {

        Time.timeScale = 0;
        setDialog(newDialog);
        speaking = true;
        canvas.SetActive(speaking);
        ShowSentence(dialog.GetCurrentSentence());
    }

    public void EndDialog() {
        Time.timeScale = 1;
        speaking = false;
        canvas.SetActive(speaking);
        OnDialogEnded.Invoke();
    }

    public void setDialog(DialogData newDialog) {
        dialog = newDialog;
    }

    private void ShowSentence(Sentence sentenceToShow) {
        portrait.sprite = sentenceToShow.character.sprite;
        characterName.text = sentenceToShow.character.characterName;
        text.TypeText(sentenceToShow.text, sentenceToShow.secondsPerCharacter);
    }
}
