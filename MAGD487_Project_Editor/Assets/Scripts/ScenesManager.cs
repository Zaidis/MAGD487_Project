using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void LoadScene(string levelName)
    {
        if (levelName == "Quit")
            Application.Quit();

        SceneManager.LoadScene(levelName);
    }
}
