using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustceneEnd : Cutscene
{
    public Image darkImage;
    public float fadeSpeed = 0.07f;

    private IEnumerator fadeCoroutine = null;
    public override void Begin() {
        base.Begin();
        if (fadeCoroutine == null)
        {
            fadeCoroutine = ShowAndFade();
            StartCoroutine(fadeCoroutine);
        }
    }

    private IEnumerator ShowAndFade() {
        darkImage.transform.parent.gameObject.SetActive(true);
        darkImage.color = new Color(
            darkImage.color.r,
            darkImage.color.g,
            darkImage.color.b,
            0f
        );
        while (darkImage.color.a < 1.0f)
        {
            darkImage.color = new Color(
                darkImage.color.r,
                darkImage.color.g,
                darkImage.color.b,
                Mathf.Clamp(darkImage.color.a + fadeSpeed * Time.deltaTime, 0f, 1f)
            );
            yield return new WaitForEndOfFrame();
        }
        DialogController.getInstance().BeginDialog(dialog);
        DialogController.getInstance().OnDialogEnded.AddListener(
            () => SceneController.LoadMainMenu()
        );
        fadeCoroutine = null;
    }
}
