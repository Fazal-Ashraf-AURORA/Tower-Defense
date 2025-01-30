#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class UI : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
