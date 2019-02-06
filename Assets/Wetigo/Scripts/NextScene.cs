using System.Collections;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    public void GotoMainScene()
    {
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("videoPlayer");
    }
}