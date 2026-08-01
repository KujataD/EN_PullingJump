using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // インスペクターで遷移先のシーン名を指定する
    [SerializeField] private string sceneName_;

    public void Change()
    {
        // 指定されたシーンへ遷移する
        SceneManager.LoadScene(sceneName_);
    }
}
