using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Background : MonoBehaviour
{
    public Image[] bgs;
    public Image bg;
    public Image[] buttons;
    public Text[] buttonText;
    public Text score;
    int counter = 0;
    bool delete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delete)
        {
            if (buttons[0].color.a == 0)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].gameObject.SetActive(false);
                    buttonText[i].gameObject.SetActive(false);
                }
            }
        }
       
    }
    public void incBg() {
        bgs[counter].DOFade(1, .5f).SetEase(Ease.Linear).SetAutoKill();
        counter++;
    }
    public void decBg()
    {
        counter--;
        if (counter >= 0)
            bgs[counter].DOFade(0, 1f).SetEase(Ease.InOutBounce).SetAutoKill();
        else
            counter = 0;
    }
    public void fadeOut()
    {
        bg.DOFade(0, 1f).SetAutoKill() ;
    }
    public void butFadeIn(int scoreText)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
           
            delete = false;
            buttons[i].gameObject.SetActive(true);
            buttonText[i].gameObject.SetActive(true);
            buttons[i].DOFade(1, .5f).SetAutoKill() ;
            buttonText[i].DOFade(1, .5f).SetAutoKill();
        }
        score.text = score.text + scoreText;
        Sequence scoreSeq = DOTween.Sequence();
        scoreSeq.AppendInterval(1f);
        scoreSeq.Append( score.DOFade(1, 1f).SetAutoKill());
        scoreSeq.SetAutoKill();
    }
    public void butFadeOut()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].DOFade(0, .5f);
            buttonText[i].DOFade(0, .5f);
            delete = true;
        }
        for(int i=0;i<bgs.Length;i++)
        bgs[i].DOFade(0, 0.5f).SetAutoKill();
    }
}
