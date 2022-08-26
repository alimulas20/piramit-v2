using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    public Image bg;
    public Image button;
    public Image button2;
    public Text text;
    public Text text2;

    // Start is called before the first frame update
    void Start()
    {
        fadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (bg.color.a == 0)
            gameObject.SetActive(false);
    }
    public void close()
    {
        button.DOFade(0, 0.25f).SetAutoKill();
        text.DOFade(0, 0.25f).SetAutoKill();
        button2.DOFade(0, 0.25f).SetAutoKill();
        text2.DOFade(0, 0.25f).SetAutoKill();
        bg.DOFade(0, 1f).SetAutoKill();
        
    }
    public void fadeIn()
    {
        Sequence myseq = DOTween.Sequence();
        myseq.AppendInterval(1f);
        myseq.Append(button.DOFade(1, .5f).SetAutoKill());
        myseq.Join(text.DOFade(1, 0.5f).SetAutoKill());
        myseq.Join(button2.DOFade(1, .5f).SetAutoKill());
        myseq.Join(text2.DOFade(1, 0.5f).SetAutoKill());
    }
}
