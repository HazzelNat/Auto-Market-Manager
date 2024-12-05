using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarController: MonoBehaviour
{
    [SerializeField] private Image progressBarImage;

    public void SetProgressAmount(float progress)
    {
        // Clamp progress between 0 and 1
        progressBarImage.fillAmount = Mathf.Clamp01(progress);
    }

    public IEnumerator FillProgressOverTime(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate progress
            float progress = elapsedTime / duration;
            
            // Update progress bar
            SetProgressAmount(progress);

            // Wait for next frame
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure progress reaches 100%
        SetProgressAmount(1f);
    }
}