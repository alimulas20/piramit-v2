using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollContent : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] elements;
    private Text[] newElements;
    int max = 8;
    int min = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void strech()
    {
            newElements = new Text[elements.Length];
            Vector3 position = elements[elements.Length - 1].transform.localPosition;
            elements[0].transform.localPosition = new Vector3(position.x, position.y - 150, position.z);
            newElements[elements.Length - 1] = elements[0];
            for (int i = 0; i < elements.Length - 1; i++)
            {
                newElements[i] = elements[i + 1];
            }
            elements = newElements;
            max = max + 1;
            min += 1;
        


    }
    public void press()
    {
        newElements = new Text[elements.Length];
        Vector3 position = elements[0].transform.localPosition;
        elements[elements.Length-1].transform.localPosition = new Vector3(position.x, position.y + 150, position.z);
        newElements[0] = elements[elements.Length-1];
        for (int i = 1; i < elements.Length; i++)
        {
            newElements[i] = elements[i - 1];
        }
        elements = newElements;
        min = min - 1;
        max -= 1;
    }
    public Vector2 Pos()
    {
        return transform.localPosition;
    }
    public void setPos( Vector2 pos)
    {
        transform.localPosition = pos;
    }
    public int getMax()
    {
        return max;
    }
    public int getMin()
    {
        return min;
    }
    
}
