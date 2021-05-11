using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneIntro : MonoBehaviour
{
    public Image darkCanvas;
    public DialogData dialog;
    public float fadeSpeed = 0.07f;
    private void Start() {
        DialogController.getInstance().BeginDialog(dialog);
        DialogController.getInstance().OnDialogEnded.AddListener(
            delegate
            {
                StartCoroutine(FadeAndShow());
            }
        );
    }

    private IEnumerator FadeAndShow() {
        while (darkCanvas.color.a > 0.0f)
        {
            darkCanvas.color = new Color(
                darkCanvas.color.r,
                darkCanvas.color.g,
                darkCanvas.color.b,
                Mathf.Clamp(darkCanvas.color.a - fadeSpeed * Time.deltaTime, 0f, 1f)
            );
            yield return new WaitForEndOfFrame();
        }
        Destroy(darkCanvas.gameObject);
        Destroy(gameObject);
    }

}
