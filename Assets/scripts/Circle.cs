using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{

   
    Image image;
    Sequence myseq;
    RectTransform rect;

    void Start()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
       
    }
   
    private void Update()
    { 
       
    }
    public void setXY(int x,int y)
    {
        rect.DOSizeDelta(new Vector2(x, y), .5f);
        
    }
    public void fade()
    {
        myseq = DOTween.Sequence();
        myseq.AppendInterval(1f);
        myseq.Append(image.DOFade(1, 1f));
        myseq.AppendInterval(.5f);
        myseq.Append(image.DOFade(0, 1f));
        myseq.SetLoops(5);

    }
    public void killFade()
    {
        myseq.Kill(false);
        image.DOFade(0, .1f);
    }
    public void setPos(Vector2 position)
    {
        transform.DOLocalMove(position,.5f);
    }
    
}