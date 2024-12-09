using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingBarController : AbstractBarController
{
    [SerializeField] private GameObject _button;
    private AsyncOperation _level;

    private new void Start()
    {
        base.Start();

        LoadScene(SceneChanger.TakeSceneID());
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneCoroutine(sceneId));
    }

    public void JoinToScene()
    {
        _level.allowSceneActivation = true;
    }

    private IEnumerator LoadSceneCoroutine(int sceneId)
    {
        _level = SceneManager.LoadSceneAsync(sceneId);
        _level.allowSceneActivation = false;

        while(_level.progress != 0.9f)
        {
            SetValue.Invoke(_level.progress * 100);

            yield return null;
        }

        SetValue.Invoke(1 * 100);
        _button.SetActive(true);
    }
}
