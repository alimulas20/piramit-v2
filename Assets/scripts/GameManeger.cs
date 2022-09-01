using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameManeger : MonoBehaviour
{
    public Timer slider;
    public Number[] numbers;
    public Operation[] operations;
    public NumberScroll left;
    public NumberScroll right;
    public Image[] clicker;
    public Background bg;
    public GameObject onay;
    public Image win;
    public Image lose;
    bool pickWaiter;
    public Circle circle;
    public Circle circle2;
    int level = 0;
    int[] values;
    int []ques;
    string[] quesText;
    int quesNumber;
    int type;
    int score;
    Vector2 [] position;
    int scrollNumber=-1;
    bool start;
    
    bool pickStoper;
    bool isPicked;
    Sequence clickerSeq;
    int [] scrollTemp;
    float timer;
    //bool wrongAns;
    int[,] near;
    Vector2[,] circlePos;

    public bool timeless;
    bool finish;
    // Start is called before the first frame update
    void Start()
    {
        first();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finish)
        {
            if (pickWaiter)
            {
                    numberGenerator();
                pickWaiter = false;
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                if (System.Array.IndexOf(ques, i) == -1)
                    numbers[i].setText(values[i]);
            }
            if (scrollNumber != -1)
            {
                if (left.getValue() != 0)
                    numbers[scrollNumber].setText(left.getValue() + "" + right.getValue());
                else
                {
                    if (!start || right.getValue() != 0 || left.getValue() != 0)
                    {
                        numbers[scrollNumber].setText(right.getValue());
                        start = false;
                    }

                }
            }
            if (!timeless && slider.finish)
            {
                gameFinish();
            }
        }
        
    }
    
    public void pick()
    {
        if (!pickStoper)
        {
            scrollNumber = -1;
            isPicked = true;
            if (type == 0)
            {
                if (values[ques[0]] ==numbers[ques[0]].getValue() )
                {
                    if (level != 0)
                    {
                        winLoseFade(true);
                        slider.incTime();
                        bg.incBg();
                        score += level * 100;
                    }  
                    level++;
                    pickWaiter = true;
                }
                else
                {
                    pickWaiter = true;
                    slider.decTime();
                    bg.decBg();
                    winLoseFade(false);
                }
            }else if (type==1)
            {
                
                if (numbers[quesNumber].getValue() != values[quesNumber])
                {
                   
                    if (level != 1)
                    {
                        //wrongAns = true;
                        StopCoroutine(manage(3));
                        winLoseFade(false);
                        slider.decTime();
                        bg.decBg();
                        pickWaiter = true;
                        
                    }
                   

                }
                if (numbers[ques[2]].getValue()==values[ques[2]])
                {
                   
                    if (level != 1&&level!=15)
                    {
                        slider.incTime();
                        bg.incBg();
                        score += level * 100;
                        winLoseFade(true);
                    }
                    level++;
                    if (level == 16)
                    {
                        finish = true;
                        gameFinish();
                    }
                    else
                    {
                        pickWaiter = true;
                    }
                  
                }
            }
            else if(type==2)
            {
                if (numbers[quesNumber].getValue() != values[quesNumber])
                {
                    if (level != 2)
                    {
                        //wrongAns = true;
                        StopCoroutine(manage(3));
                        slider.decTime();
                        bg.decBg();
                        winLoseFade(false) ;
                    }
                   
                    pickWaiter = true;
                  

                }
                if (numbers[ques[5]].getValue() == values[ques[5]])
                {
                    if (level != 2)
                    {
                        winLoseFade(true);
                        slider.incTime();
                        bg.incBg();
                        score += level * 100;
                    }
                       
                    level++;
                    pickWaiter = true;
                }
            }
            
        }

    }
    void numberGenerator()
    {
        switch (level)
        {
            case 0:
                slider.stop();
                levelCreator(5, 5, false);
                StartCoroutine(howToPlay(0));
                break;
            case 1:
                levelCreator(5, 5,5,false);
                StartCoroutine(howToPlay(2));
                break;
            case 2:
                circle.killFade();
                circle2.killFade();
                left.setInfinite();
                right.setInfinite();
                left.release();
                right.release();
                slider.play();
                stopFade(1);
                levelCreator(5, 5, false);
                StartCoroutine(manage(0));
                break;
            case 3:
                levelCreator(5, 5, 5, false);
                StartCoroutine(manage(2));
                break;
            case 4:
                levelCreator(5, 5, 5, 5);
                StartCoroutine(manage(5));
                break;
            case 5:
                levelCreator(10, 5, false);
                StartCoroutine(manage(0));
                break;
            case 6:
                levelCreator(10, 5, 10, false);
                StartCoroutine(manage(2));
                break;
            case 7:
                levelCreator(10, 5, 5, 10);
                StartCoroutine(manage(5));
                break;
            case 8:
                levelCreator(10, 10, false);
                StartCoroutine(manage(0));
                break;
            case 9:
                levelCreator(10, 10, 10, false);
                StartCoroutine(manage(2));
                break;
            case 10:
                levelCreator(10, 10, 10, 10);
                StartCoroutine(manage(5));
                break;
            case 11:
                levelCreator(20, 20, false);
                StartCoroutine(manage(0));
                break;
            case 12:
                levelCreator(20, 20, 20, false);
                StartCoroutine(manage(2));
                break;
            case 13:
                levelCreator(5, 5,true);
                StartCoroutine(manage(0));
                break;
            case 14:
                ////////////////////////çýkarýlacak
                left.setInfinite();
                right.setInfinite();
                left.release();
                right.release();
                slider.play();
                stopFade(1);
                ///////////////////////////////////////
                levelCreator(5, 5,5, true);
                StartCoroutine(manage(2));
                break;
            default:
                break;

        }
    }
    IEnumerator manage(int quesCount)
    {
    
        for (int i = 0; i <= quesCount; i++)
        {
            pickStoper = true;
            isPicked = false;
            scrollNumber = -1;
            quesNumber = ques[i];
            numbers[ques[i]].selected();
            
            setChange();
            setTimer();
            if (level < 6)
            {
               
                yield return new WaitWhile(() => !isChange() && !isTimeUp(2));
                if (isTimeUp(2))
                {
                    numbers[ques[i]].setText(quesText[i]);
                    pickStoper = true;
                }
                else
                {
                    pickStoper = false;
                }
                
            }
            yield return new WaitWhile(() => !isChange());
            pickStoper = false;
            scrollNumber = ques[i];
            numbers[ques[i]].KillSelect();
            yield return new WaitWhile(() => !isPicked);
            isPicked = false;
            /*if (wrongAns)
            {
                Debug.Log("abc");
                wrongAns = false;
                break;
            }*/
            
        }
        

    }
    void levelCreator(int max1, int max2, bool mult)
    {
        operations[0].returnOrigin();
        operations[0].reSize(80,80);
        if (mult)
        {
            operations[0].rotate();
        }
        else
        {

            operations[0].backRotate();
        }
        for (int i=0; i < numbers.Length; i++)
        {
            numbers[i].setTextSize(100);
            if (i > 0 && i < 3)
            {
                numbers[i].setPosition(200 * (int)Mathf.Pow(-1, i), -275);
                position[i] = new Vector2(200 * (int)Mathf.Pow(-1, i), -275);
            }
            if (i < 3)
            {
                numbers[i].reSize(300, 150);
               
            }
            else
            {
                numbers[i].fadeOut();
            }
        }
        for(int i = 1; i < operations.Length; i++)
        {
            operations[i].fadeOut();
        }
        for(int i = 0; i < ques.Length; i++)
        {
            ques[i] = -1;
        }
        type = 0;
        string[] oper=new string[2];
        values[2] = Random.Range(1, max1);
        values[1] = Random.Range(1, max2);
        if (!mult)
        {
            oper[0] = "+";
            oper[1] = "-";
            values[0] = values[1] + values[2];
        }
        else
        {
            oper[0] = "*";
            oper[1] = "/";
            values[0] = values[1] * values[2];
        }
        ques[0] = Random.Range(0, 3);
        numbers[ques[0]].setText("?");
        if (ques[0] == 0)
        {
            quesText[0] = concatOpe(1, 2, oper[0]);
        }
        else if (ques[0] == 1)
        {
            quesText[0] = concatOpe(0, 2, oper[1]);
        }
        else
        {
            quesText[0] = concatOpe(0, 1, oper[1]);
        }

    }
   void levelCreator(int max1, int max2,int max3, bool mult)
    {
        type = 1;
        incLevel(mult);   
      
        string[] oper = new string[2];
        values[5] = Random.Range(1, max1);
        values[4] = Random.Range(1, max2);
        values[3] = Random.Range(1, max3);
        if (!mult)
        {
            oper[0] = "+";
            oper[1] = "-";
            values[2] = values[4] + values[5];
            values[1] = values[3] + values[4];
            values[0] = values[1] + values[2];
        }
        else
        {
            oper[0] = "*";
            oper[1] = "/";
            values[2] = values[4] * values[5];
            values[1] = values[3] * values[4];
            values[0] = values[1] * values[2];
        }
        

        int quesType;
        if (level < 5)
        {
            quesType = Random.Range(0, 4);
        }
        else
        {
            quesType = Random.Range(1, 3);
        }
        if (quesType == 0)
        {
            for (int i = 0; i < ques.Length; i++)
                ques[i] = 2 - i;
            quesText[0] = concatOpe(4, 5, oper[0],0);
            quesText[1] = concatOpe(3, 4 , oper[0],1);
            quesText[2] = concatOpe(1, 2, oper[0],2);
        }
        else if (quesType == 1)
        {
            ques[0] = 1;
            ques[1] = 4;
            ques[2] = 3;
            quesText[0] = concatOpe(0, 2, oper[1],0);
            quesText[1] = concatOpe(2, 5, oper[1],1);
            quesText[2] = concatOpe(1, 4, oper[1],2);
        }
        else if (quesType == 2)
        {
            ques[0] = 2;
            ques[1] = 4;
            ques[2] = 5;
            quesText[0] = concatOpe(0, 1, oper[1], 0);
            quesText[1] = concatOpe(1, 3, oper[1], 1);
            quesText[2] = concatOpe(2, 4, oper[1], 2);
        }
        else
        {
            ques[0] = 0;
            ques[1] = 5;
            ques[2] = 3;
            quesText[0] = concatOpe(1, 2, oper[0], 0);
            quesText[1] = concatOpe(2, 4, oper[1], 1);
            quesText[2] = concatOpe(1, 4, oper[1], 2);
        }
        for(int i = 0; i < 3; i++)
        {
            numbers[ques[i]].setText("?");
        }
        
       
       

    }
    void levelCreator(int max1, int max2,int max3,int max4)
    {
        type = 2;
        incLevel(false);
        type = 2;
        string[] oper = new string[2];
        values[9] = Random.Range(1, max1);
        values[8] = Random.Range(1, max2);
        values[7] = Random.Range(1, max3);
        values[6] = Random.Range(1, max4);
        oper[0] = "+";
        oper[1] = "-";
        values[5] = values[8] + values[9];
        values[4] = values[7] + values[8];
        values[3] = values[6] + values[7];
        values[2] = values[4] + values[5];
        values[1] = values[3] + values[4];
        values[0] = values[1] + values[2];
        int quesType =Random.Range(0, 2);
        if (quesType == 0)
        {
            ques[0] = 2;//0-1
            ques[1] = 4;//1-3
            ques[2] = 5;//2-4
            ques[3] = 7;//3-6
            ques[4] = 8;//4-7
            ques[5] = 9;//5-8
            for(int i = 0; i < 6; i++)
            {
                if (i ==0)
                quesText[i] = concatOpe(0, 1, "-",i);
                else if (i < 3)
                {
                    quesText[i] = concatOpe(i, i+2, "-",i);

                }
                else
                {
                    quesText[i] = concatOpe(i, i+3, "-",i);
                }
            }
        }
        else
        {
            ques[0] = 1;//0-2
            ques[1] = 4;//2-5
            ques[2] = 3;//1-4
            ques[3] = 8;//5-9
            ques[4] = 7;//4-8
            ques[5] = 6;//3-7
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                    quesText[i] = concatOpe(i, 2, "-",i);
                else if (i < 3)
                {
                    quesText[i] = concatOpe(3-i, 6-i, "-",i);

                }
                else
                {
                    quesText[i] = concatOpe(8-i, 12-i, "-",i);
                }
            }
        }
       
        for (int i = 0; i < 6; i++)
        {
            numbers[ques[i]].setText("?");
        }

        
    }
    bool isChange()
    {
        if (scrollTemp[0] != left.getValue())
            return true;
        if (scrollTemp[1] != right.getValue())
            return true;
        return false;
    }
    void setChange()
    {
        scrollTemp[0] = left.getValue();
        scrollTemp[1] = right.getValue();
    }
    bool isTimeUp(int i)
    {
        
        if (timer + i < Time.time)
            return true;
        return false;
    }
   void setTimer()
    {
        timer = Time.time;
    }
   void clickerFade( int i)
    {
        clickerSeq = DOTween.Sequence();
        clickerSeq.Append(clicker[i].DOFade(1, 1f)).SetEase(Ease.Linear).SetAutoKill();
        clickerSeq.AppendInterval(1f);
        clickerSeq.Append(clicker[i].DOFade(0, 1f)).SetEase(Ease.Linear).SetAutoKill();
        clickerSeq.SetLoops(10);
    }
    void stopFade(int i)
    {
        clickerSeq.Kill(true);
        clicker[i].DOFade(0, 0.25f).SetAutoKill(); ;
    }
    void autoPick(int i)
    {
        left.setValue(values[i] / 10);
        right.setValue(values[i] % 10);
    }
    IEnumerator howToPlay(int quesCount)
    {
        
        for(int i = 0; i <= quesCount; i++)
        {
            circle.killFade();
            circle2.killFade();
            pickStoper = true;
            stopFade(1);
            isPicked = false;
            if (level == 0)
            {
                numbers[ques[i]].selected(true);
                circle.setXY(300, 200);
                circle2.setXY(750, 200);
                circle.setPos(numbers[0].transform.localPosition);
                circle2.setPos(new Vector2((numbers[1].transform.localPosition.x+ numbers[2].transform.localPosition.x)/2,numbers[1].transform.localPosition.y));
            }
            else
            {
                circle.setPos(circlePos[i, 0]);
                circle2.setPos(circlePos[i, 1]);
                circle.setXY(210,140);
                circle2.setXY(500, 140);
                numbers[ques[i]].selected();
                numbers[near[i,0]].nearFade();
                numbers[near[i, 1]].nearFade();
                
            }
           
           
            left.release();
            right.release();
            numbers[ques[i]].setText(quesText[i]);
            setChange();
            setTimer();
            clickerFade(0);
            yield return new WaitWhile(() => !isChange() && !isTimeUp(5));
            circle.fade();
            circle2.fade();
            numbers[ques[i]].KillSelect();
            numbers[near[i, 0]].killNearFade();
            numbers[near[i, 1]].killNearFade();
            if (isChange())
            {
                scrollNumber = ques[i];
                yield return new WaitWhile(() => !isTimeUp(8) && numbers[ques[i]].getValue() != values[ques[i]]);
                autoPick(ques[i]);
            }
            else
            {
                scrollNumber = ques[i];
                autoPick(ques[i]);
            }
            
            stopFade(0);
            pickStoper = false;
            clickerFade(1);
            setTimer();
            yield return new WaitForSeconds(.5f);
            scrollNumber = -1;
            yield return new WaitWhile(() => !isPicked && !isTimeUp(5));
            stopFade(1);
           
        }
       
    }
    void incLevel(bool mult)
    {
        if (type == 1)
        {
            for(int i = 0; i < 6; i++)
            {
                numbers[i].reSize(200, 125);
                numbers[i].fadeIn();
                if (i > 0 && i < 3)
                {
                    numbers[i].setPosition(145 *(int) Mathf.Pow(-1, i), -230);
                    position[i] = new Vector2(145 * (int)Mathf.Pow(-1, i), -230);
                }
                if (i > 2)
                {
                    
                    position[i] = numbers[i].returnOrigin();
                }
            }

            operations[0].setPositionY(-230);
            for(int i = 1; i < 3; i++)
            {
                operations[i].reSize(80, 80);
                operations[i].gameObject.SetActive(true);
                operations[i].returnOrigin();
                operations[i].fadeIn();
            }
            if (mult)
            {
                
                for(int i = 0; i < 3; i++)
                {
                    operations[i].rotate();
                    
                }
            }
            else
            {
                
                for (int i = 0; i < 3; i++)
                {
                    operations[i].backRotate();
                }
            }
        }
        if (type == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                numbers[i].setTextSize(50);
                numbers[i].reSize(150, 100);
                numbers[i].fadeIn();
                if (i > 0 && i < 3)
                {
                    numbers[i].setPosition(105 * (int)Mathf.Pow(-1, i), -220);
                    position[i] = new Vector2(105 * (int)Mathf.Pow(-1, i), -220);
                }else if (i>0&&i < 6)
                {
                    numbers[i].setPosition(210 *(i-4) , -340);
                    position[i] = new Vector2(210 * (i - 4), -340);
                }
            }
            for(int i = 0; i < operations.Length; i++)
            {
                operations[i].reSize(50, 50);
            }
            operations[0].setPositionY(-220);
            operations[1].setPosition(-105,-340);
            operations[2].setPosition(105,-340);
            operations[3].gameObject.SetActive(true);
            operations[4].gameObject.SetActive(true);
            operations[5].gameObject.SetActive(true);
            operations[3].fadeIn();
            operations[4].fadeIn();
            operations[5].fadeIn();
        }
    }
    string concatOpe(int first,int second,string ope)
    {
            return values[first] + ope + values[second];
       
        
    }
    string concatOpe(int first, int second, string ope,int index)
    {
        if (ope == "+")
        {
            circlePos[index, 0] = position[ques[index]];
            circlePos[index, 1] = new Vector2((position[first].x + position[second].x)/2, position[second].y);
        }
        else
        {
            circlePos[index, 0] = position[first];
            circlePos[index, 1] = new Vector2((position[ques[index]].x + position[second].x) /2, position[second].y);
        }
        near[index, 0] = first;
        near[index, 1] = second;
        return values[first] + ope + values[second];


    }
    public void sliderOut()
    {
        slider.fadeOut();
    }
    public void gameFinish()
    {
        finish = true;
        slider.fadeOut();
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].fadeOut();
            numbers[i].KillSelect();
            numbers[near[i, 0]].killNearFade();
            numbers[near[i, 0]].killNearFade();
        }
        for (int i = 0; i < operations.Length; i++)
        {
            operations[i].fadeOut();
        }
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
        onay.SetActive(false); 
        bg.butFadeIn(score);

    }
    public void Restart(bool timeless)
    {
        level = 0;
        finish = false;
        score = 0;
        if (timeless)
            slider.fadeOut();
        else
        {
            slider.fadeIn();
            slider.play();
        }
          
        for (int i = 0; i < 3; i++)
        {
            numbers[i].fadeIn();
        }
        for (int i = 0; i < 1; i++)
        {
            operations[i].fadeIn();
        }
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        onay.SetActive(true);
        isPicked = false;
        first();  
    }
    void first()
    {
        
        circlePos = new Vector2[numbers.Length,2];
        near = new int[numbers.Length,2];
        pickWaiter = true;
        ques = new int[numbers.Length];
        quesText = new string[numbers.Length];
        for (int i = 0; i < ques.Length; i++)
        {
            ques[i] = -1;
        }
        position = new Vector2[numbers.Length];
        for (int i = 0; i < position.Length; i++)
        {
            position[i] = numbers[i].transform.localPosition;
        }
        values = new int[numbers.Length];
        type = 0;
        start = true;
        scrollTemp = new int[2];
        pickStoper = true;
        quesNumber = 0;
    }
    void winLoseFade(bool winer)
    {
        if (winer)
        {
            
            Sequence winLose = DOTween.Sequence();
            winLose.Append( win.DOFade(1, .5f));
            winLose.AppendInterval(.5f);
            winLose.Append(win.DOFade(0, .5f));
           
            
        }
        else
        {
            Sequence winLose = DOTween.Sequence();
            winLose.Append(lose.DOFade(1, .5f));
            winLose.AppendInterval(0.5f);
            winLose.Append(lose.DOFade(0, .5f));
            
        }
    }
}
