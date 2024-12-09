using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IButton
{
    public void OnClick()
    {
        SceneChanger.AddSceneID(2);

        SceneManager.LoadScene(1);
    }
}
