using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManeger gameManager;
    public Intro intro;
    void Start()
    {
        intro.fadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void play()
    {
        intro.close();
        gameManager.gameObject.SetActive(true);
        gameManager.timeless = false;
        gameManager.Restart(false);
    }
    public void timeless()
    {
        intro.close();
        gameManager.gameObject.SetActive(true);
        gameManager.sliderOut();
        gameManager.timeless = true;
        gameManager.Restart(true);
    }
}
