using UnityEngine;
using System.Collections;

public class TaskProgressManager : MonoBehaviour
{
    [SerializeField] private ProgressBarController progressBar;
    [SerializeField] private GameObject progressBarObject;

    void Start()
    {
        if (progressBarObject != null)
        {
            progressBarObject.SetActive(false);
        }
    }

    public void ShowProgressBar(float duration)
    {
        if (progressBarObject != null)
        {
            progressBarObject.SetActive(true);
            progressBar.SetProgressAmount(0); // Start at 0
            StartCoroutine(FillProgressBar(duration));
        }
    }

    private IEnumerator FillProgressBar(float duration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            
            if (progressBar != null)
            {
                progressBar.SetProgressAmount(progress);
            }
            
            yield return null;
        }

        // Make sure it hits 100%
        if (progressBar != null)
        {
            progressBar.SetProgressAmount(1f);
            yield return new WaitForSeconds(0.2f); // Show full progress briefly
            progressBarObject.SetActive(false);
        }
    }
}