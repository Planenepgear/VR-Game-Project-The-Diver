using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerL1 : MonoBehaviour
{
    public void GameOver()
    {
        Invoke(nameof(BackToMain), 2f);
    }

    private void BackToMain()
    {
        SceneManager.LoadScene("0-Main Scene", LoadSceneMode.Single);
    }
}
