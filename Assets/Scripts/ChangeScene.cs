using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void changeScene() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame() {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}
