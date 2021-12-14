using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    public static string fromMenuLoad;
    string levelToLoad;
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
        levelToLoad = levelName;
        SceneTransitioner.instance.Transition();
    }
    public static void SetMenuLoad(string s)
    {
        fromMenuLoad = s;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
