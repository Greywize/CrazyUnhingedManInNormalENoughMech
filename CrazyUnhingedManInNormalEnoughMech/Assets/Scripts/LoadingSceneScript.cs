using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneScript : MonoBehaviour
{
    public GameObject loadingPanel;
    //public Slider loadingBar;
    //public TextMeshProUGUI loadingPercentage;
    public string levelName = "sceneNameHere";

    void Start()
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync(string lvlN)
    {
        loadingPanel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(lvlN);

        while (!op.isDone)
        {
            //float progress = Mathf.Clamp01(op.progress / .9f);
            //loadingBar.value = progress;
            //loadingPercentage.text = (int)progress * 100 + "%";
            yield return null;
        }
    }
}
