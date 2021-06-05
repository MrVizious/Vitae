using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneIntro : Cutscene
{
    public Image darkImage;
    public float fadeSpeed = 0.07f;
    private void Start() {
        Begin();
        DialogController.getInstance().BeginDialog(dialog);

        DialogController.getInstance().OnDialogEnded.AddListener(
            delegate
            {
                StartCoroutine(FadeAndShow());
            }
        );
    }

    private IEnumerator FadeAndShow() {
        while (darkImage.color.a > 0.0f)
        {
            darkImage.color = new Color(
                darkImage.color.r,
                darkImage.color.g,
                darkImage.color.b,
                Mathf.Clamp(darkImage.color.a - fadeSpeed * Time.deltaTime, 0f, 1f)
            );
            yield return new WaitForEndOfFrame();
        }
        //Destroy(darkImage.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
