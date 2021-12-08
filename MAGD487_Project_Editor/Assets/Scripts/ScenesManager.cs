using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
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
        player.SetActive(true);
        canvas.SetActive(true);
        SceneManager.LoadScene(levelName);
    }
}
