using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class NumberScroll : MonoBehaviour
{
    public ScrollContent left;
    public ScrollRect myScroll;
    
    private int positionLeft;


    bool dragLeft = false;

    bool stopDrag;
    bool infinite;


    void Start()
    {
        positionLeft = (((int)left.Pos().y + 73) / 150);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopDrag)
        {
            if (dragLeft)
            {

                positionLeft = ((int)left.Pos().y + 30) / 150;
                if (infinite)
                {
                    if (positionLeft > left.getMax())
                    {
                        left.strech();
                    }
                    if (positionLeft < left.getMin())
                    {
                        left.press();
                    }
                }
               
            }
        }

        
        if (!dragLeft)
        {
            lerpLeft(positionLeft * 150);
        }
    }
    public void StartDragLeft()
    {
        dragLeft = true;
    }
    public void EndDragLeft()
    {
        dragLeft = false;
    }
   
    void lerpLeft(int position)
    {
        float newY = Mathf.Lerp(left.Pos().y, position, Time.deltaTime * 30f);
        Vector2 newPos = new Vector2(left.Pos().x, newY);
        left.setPos(newPos);
    }
    public int getValue()
    {
        if (positionLeft % 10 < 0)
        {
            return 10 + positionLeft % 10;
        }
        else
        {
            return positionLeft % 10;
        }
    }
    public void setValue(int number)
    {
        positionLeft = number;
        stopDrag = true;
    }
    public void release()
    {
        stopDrag = false;
    }
    public void setInfinite()
    {
        infinite = true;
        myScroll.movementType = ScrollRect.MovementType.Unrestricted;
    }
}
