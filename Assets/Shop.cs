using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int childAmount;
    private int showingChilds = 6;
    // Start is called before the first frame update
    void Start()
    {
        childAmount = transform.childCount;
        childAmount -= showingChilds;

    }
    public void ScrollbarValueChange(float value)
    {
        int intValue = (int)Mathf.Ceil(childAmount * value);
        if(intValue != 0)
        {
            transform.GetChild(intValue - 1).gameObject.SetActive(false);
        }
        transform.GetChild(intValue + showingChilds - 1).gameObject.SetActive(true);
        transform.GetChild(intValue).gameObject.SetActive(true);
        if (intValue != childAmount)
        {
            transform.GetChild(intValue + showingChilds).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
