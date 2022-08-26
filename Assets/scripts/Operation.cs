using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Operation : MonoBehaviour
{
    RectTransform ope;
    bool closed;
    public Image image;
    Vector2 origin;
    // Start is called before the first frame update
    void Start()
    {
        ope = GetComponent<RectTransform>();
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void reSize(int x, int y)
    {
        ope.DOSizeDelta(new Vector2(x, y), 1f).SetAutoKill();   
    }
    public void fadeOut()
    {
        image.DOFade(0, 1f).SetAutoKill();
       
        closed = true;
    }
    public void fadeIn()
    {
        closed = false;
        image.DOFade(1, 1f).SetAutoKill();
    }
    public bool isClosed()
    {
        return closed;
    }
    public void setPosition(int x, int y)
    {
        ope.DOLocalMove(new Vector2(x, y), 0.5f).SetAutoKill();
    }
    public void setClosed()
    {
        closed = true;
    }
    public void setPositionY(int y)
    {
        ope.DOLocalMoveY(y, 0.5f);
    }
    public void returnOrigin()
    {
        ope.DOLocalMove(origin,0.5f).SetAutoKill();
    }
   public void rotate()
    {
        transform.DORotate(new Vector3(0, 0, 45), 1f).SetAutoKill();
    }
    public void backRotate()
    {
        transform.DORotate(new Vector3(0, 0, 0), 1f).SetAutoKill();
    }
}
