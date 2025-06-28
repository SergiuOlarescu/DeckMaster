using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnExitPressed()
    {
        Debug.Log("Exit pressed");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
