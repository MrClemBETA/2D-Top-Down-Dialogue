using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string destinationScene;
    public string areaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D()
    {
        Player.instance.areaTransitionName = areaTransitionName;
        Player.instance.SetTransitioning(true);
        HUD.instance.FadeSceneOut(destinationScene);
    }

}
