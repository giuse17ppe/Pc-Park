using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class SubtitleManager : MonoBehaviour
{
    public TMP_Text subtitleText; // Riferimento al testo dei sottotitoli
    public string introSubtitle; // Sottotitolo di introduzione
    public string[] subtitles; // Array dei sottotitoli per ogni fase
    public float fadeDuration = 1f; // Durata del fade-in/out
    public float displayDuration = 2f; // Durata di visualizzazione di ogni sottotitolo

    private int currentSubtitleIndex = 0; // Traccia il sottotitolo attuale

    private void Start()
    {
        // Mostra il sottotitolo introduttivo all'avvio
        StartCoroutine(AnimateSubtitle(introSubtitle));
    }

    public void ShowNextSubtitle()
    {
        if (currentSubtitleIndex < subtitles.Length)
        {
            StartCoroutine(AnimateSubtitle(subtitles[currentSubtitleIndex]));
            currentSubtitleIndex++;
        }
    }

    private IEnumerator AnimateSubtitle(string subtitle)
    {
        // 1. Mostra il testo
        subtitleText.text = subtitle;
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);

        // 2. Animazione di fade-in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, alpha);
            yield return null;
        }
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 1);

        // 3. Attendi il tempo di visualizzazione
        yield return new WaitForSeconds(displayDuration);

        // 4. Animazione di fade-out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = 1 - (t / fadeDuration);
            subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, alpha);
            yield return null;
        }
        subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
    }
}