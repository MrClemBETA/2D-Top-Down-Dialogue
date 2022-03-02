using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public static HUD instance = null;
    public GameObject sceneFadePanel;
    public float fadeTime = 1f;

    private bool isTransitioning;
    private string destinationScene;
    private float timeElapsed = 0f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        // Get the image
        Image img = sceneFadePanel.GetComponent<Image>();

        // Check to see if fade out is complete
        if(isTransitioning && timeElapsed > fadeTime)
        {
            SceneManager.LoadScene(destinationScene);
            timeElapsed = 0;
            FadeSceneIn();
        } /*
        // Check to see if fade in is complete
        else if(isTransitioning && img.color.a <= 0)
        {
            Player.instance.SetTransitioning(false);
            isTransitioning = false;
        }*/
    }

    private void FixedCrossFadeAlpha(Image img, float alpha, float time, bool ignoreTimeScale)
    {
        Color fixedColor = img.color;
        fixedColor.a = 1;
        img.color = fixedColor;
        img.CrossFadeAlpha(0f, 0f, false);

        img.CrossFadeAlpha(alpha, time, ignoreTimeScale);
    }

    public void FadeSceneOut(string destinationScene)
    {
        this.destinationScene = destinationScene;
        isTransitioning = true;
        FixedCrossFadeAlpha(sceneFadePanel.GetComponent<Image>(), 1f, fadeTime, true);
    }

    public void FadeSceneIn()
    {
        sceneFadePanel.GetComponent<Image>().CrossFadeAlpha(0f, fadeTime, true);
    }
}
