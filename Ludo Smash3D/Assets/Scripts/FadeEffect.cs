using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    // UI panel GameObject
    public GameObject panel;

    // SceneLoader script reference
    public SceneLoader loader;

    // Scene index to load
    public int sceneIndex;

    // Fade duration in seconds
    public float fadeDuration = 1f;

    // Fade the panel in
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine(1, 0));
    }

    // Fade the panel out
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine(0, 1));
    }

    // Coroutine to fade the panel in
    private IEnumerator FadeInCoroutine(float startAlpha, float endAlpha)
    {

        float elapsedTime = 0;
        Color panelColor = panel.GetComponent<Image>().color;
        panelColor.a = startAlpha;
        panel.GetComponent<Image>().color = panelColor;

        // Update panel alpha over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            panel.GetComponent<Image>().color = panelColor;
            yield return null;
        }

        // Set final alpha
        panelColor.a = endAlpha;
        panel.GetComponent<Image>().color = panelColor;

        // Deactivate the panel
        panel.SetActive(false);
    }

    // Coroutine to fade the panel out
    private IEnumerator FadeOutCoroutine(float startAlpha, float endAlpha)
    {
        // Activate the panel
        panel.SetActive(true);

        float elapsedTime = 0;
        Color panelColor = panel.GetComponent<Image>().color;
        panelColor.a = startAlpha;
        panel.GetComponent<Image>().color = panelColor;

        // Update panel alpha over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelColor.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            panel.GetComponent<Image>().color = panelColor;
            yield return null;
        }

        // Set final alpha
        panelColor.a = endAlpha;
        panel.GetComponent<Image>().color = panelColor;


        // Load the next scene
        loader.LoadScene(sceneIndex);
    }
}
