using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour
{
    public UnityEvent OnCutsceneEnded;
    public DialogData dialog;
    public virtual void Begin() {

        //DialogController.getInstance().BeginDialog(dialog);

        DialogController.getInstance().OnDialogEnded.AddListener(() => OnCutsceneEnded.Invoke());

    }
}
