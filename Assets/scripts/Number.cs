using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Number : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    RectTransform num;
    public Image image;
    bool closed;
    Sequence myseq;
    Sequence waitSeq;
    Sequence nearSeq;
    Vector2 origin;
    void Start()
    {
        num = GetComponent<RectTransform>();
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setText(int number)
    {
        text.text = number.ToString();
    }
    public void setText(string value)
    {
        text.text = value;
    }
    public int getValue()
    {
        int value=-1;
        int.TryParse(text.text, out value);
        return value;

    }
    public void reSize(int x,int y)
    {
        num.DOSizeDelta(new Vector2(x,y), 1f).SetAutoKill();
    }
    public void fadeOut()
    {
        image.DOFade(0, 1f).SetAutoKill();
        text.DOFade(0, .25f).SetAutoKill();
        closed = true;
    }
    public void fadeIn()
    {
        closed = false;
        image.DOFade(1, .5f).SetAutoKill();
        text.DOFade(1, .75f).SetAutoKill();
    }
    public bool isClosed()
    {
        return closed;
    }
    public void setPosition(int x,int y)
    {
        num.DOLocalMove(new Vector2(x, y), 0.5f).SetAutoKill();
    }
    public void selected()
    {
        myseq = DOTween.Sequence();
        myseq.Append(image.DOColor(Color.green,1f).SetAutoKill());
        myseq.Append(image.DOColor(new Color(1,1,1), 1f).SetAutoKill());
        myseq.SetLoops(10);
    }
    public void selected(bool wait)
    {
        if (wait)
        {
           waitSeq = DOTween.Sequence();
            waitSeq.AppendInterval(1f);
            myseq = DOTween.Sequence();
            myseq.Append(image.DOColor(Color.green, 1f).SetAutoKill());
            myseq.Append(image.DOColor(new Color(1, 1, 1), 1f).SetAutoKill());
            myseq.SetLoops(10);
            waitSeq.Append(myseq);
        }
    }
    public void KillSelect()
    {
        waitSeq.Kill(false);
        myseq.Kill(false);
        image.DOColor(new Color(1, 1, 1), 0.5f).SetAutoKill();
    }
    public void setTextSize( int size)
    {
        text.fontSize = size;
    }
    public void inOper()
    {
        image.DOColor(Color.red,.5f);
    }
    public void outOper()
    {
        image.DOColor(new Color(1, 1, 1),.5f);
    }
    public Vector2 returnOrigin()
    {
        num.DOLocalMove(origin, 0.5f).SetAutoKill();
        return origin;
    }
    public void nearFade()
    {
        nearSeq = DOTween.Sequence();
        nearSeq.Append(image.DOColor(Color.red, 1f).SetAutoKill());
        nearSeq.Append(image.DOColor(new Color(1, 1, 1), 1f).SetAutoKill());
        nearSeq.SetLoops(10);
    }
    public void killNearFade()
    {
        nearSeq.Kill(false);
        image.DOColor(new Color(1, 1, 1), 0.5f).SetAutoKill();
    }
}
