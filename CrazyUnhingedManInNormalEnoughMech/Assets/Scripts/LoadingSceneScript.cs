using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zacks.Terrain;

public class LoadingSceneScript : MonoBehaviour
{
    public CubeGenerator terrainGen;
    public GameObject loadingPanel;
    public TextMeshProUGUI specificText;
    public TextMeshProUGUI loadingPercentage;
    public Slider loadingBar;
    public string levelName = "sceneNameHere";

    void Awake()
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync(string lvlN)
    {
        loadingPanel.SetActive(true);

        if (!terrainGen)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(lvlN);

            while (!op.isDone)
            {
                float progress = Mathf.Clamp01(op.progress / .9f);
                loadingBar.value = progress;
                loadingPercentage.text = progress * 100 + "%";
                yield return null;
            }
        }
        else
        {
            terrainGen.GenerateCubes(default, loadingPanel, specificText, loadingPercentage, loadingBar);
        }
    }
}
