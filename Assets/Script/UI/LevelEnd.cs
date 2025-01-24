using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] public static LevelEnd Instance;
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator animator;
    [SerializeField] private Image img;
    [SerializeField] private Image gameOverimg;
    [SerializeField] private Button[] buttons;
    [SerializeField] private AudioClip[] clip;
    [SerializeField] private AudioSource source;
    public bool isEnded=false;
    private void Awake()
    {
        source=GetComponent<AudioSource>();
        Instance = this;
        animator=GetComponent<Animator>();
        animator.enabled = false;
        img = GetComponent<Image>();
        img.enabled = false;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void EndLevel(int index)
    {
        if (!isEnded)
        {
            isEnded = true;
            img.enabled = true;
            panel.SetActive(true);
            animator.enabled = true;
            switch (index)
            {
                case 1:
                    gameOverimg.sprite = sprites[0];
                    gameOverimg.SetNativeSize();
                    source.PlayOneShot(clip[0]);
                    var indexBine = SceneManager.GetActiveScene().buildIndex;
                    if (indexBine < 4)
                        buttons[0].gameObject.SetActive(false);
                    else buttons[1].gameObject.SetActive(false);
                    break;
                case 2:
                    gameOverimg.sprite = sprites[1];
                    source.PlayOneShot(clip[1]);
                    gameOverimg.SetNativeSize();
                    buttons[1].gameObject.SetActive(false);
                    break;
            }
        }
    }
    public void NextLevel()
    {
        var indexBine = SceneManager.GetActiveScene().buildIndex;
        if (indexBine < 4)
            indexBine++;
        SceneManager.LoadScene(indexBine);
    }
    public void Restart()
    {
       var index= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
