using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ToggleButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void ToggleButtonEffect(GameObject panel)
    {
        if (panel.gameObject.activeSelf)
        {
            panel.GetComponent<Animator>().SetTrigger("Close");
            StartCoroutine(PanelWithDelay(0.5f, panel));
            return;
        }
        StartCoroutine(PanelWithDelay(0,panel));
    }
    IEnumerator PanelWithDelay(float duration,GameObject panel)
    {
        yield return new WaitForSeconds(duration);
        panel.SetActive(!panel.gameObject.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
