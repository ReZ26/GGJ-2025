using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
