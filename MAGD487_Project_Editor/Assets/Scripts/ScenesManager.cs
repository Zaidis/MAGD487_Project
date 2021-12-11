using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
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
        SceneTransitioner.Transition();
    }

    public void LoadScene()
    {
        player.SetActive(true);
        canvas.SetActive(true);
        SceneManager.LoadScene(levelToLoad);
        SceneTransitioner.Transition();
    }
}
