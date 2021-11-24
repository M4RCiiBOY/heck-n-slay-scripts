using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class MainMenuManager : MonoBehaviour
{
    AsyncOperation async;
    public Image progressBar;
    public GameObject loadingBar;
    public GameObject MainMenu;
    bool pressStart = false;
    [SerializeField]
    bool isSyncing = false;
    int level;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pressStart)
        {
            StartCoroutine(LoadScene(level));
        }
    }

    public void ClickStart(int lvl)
    {
        pressStart = true;
        MainMenu.SetActive(false);
        loadingBar.SetActive(true);
        isSyncing = true;
        level = lvl;
    }
    IEnumerator LoadScene(int lvl)
    {
        yield return new WaitForSeconds(1);
        if(isSyncing)
        {
            async = SceneManager.LoadSceneAsync(level);
            isSyncing = false;
        }

        while (!async.isDone)
        {
            progressBar.fillAmount = async.progress;
            Debug.Log(async.progress);
            yield return null;
        }
    }
}
