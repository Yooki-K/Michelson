using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CalcuteArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    private GameObject ShowArea;
    private Text Result;
    private Text Result2;
    private int ShowState = -1;
    public int MaxHeight = 200;
    public int MinWidth = 200;
    public int speed = 1000;
    public bool IsEnter = false;
    public Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        ShowArea = transform.parent.Find("ShowArea").gameObject;
        Result = ShowArea.transform.Find("result").GetComponent<Text>();
        Result2 = ShowArea.transform.Find("result2").GetComponent<Text>();
        ShowArea.transform.Find("Submit").GetComponent<Button>().onClick.AddListener(Submit);
        ShowArea.transform.Find("Clear").GetComponent<Button>().onClick.AddListener(Clear);
        ShowArea.SetActive(false);
    }

    private void Submit()
    {
        int i = 0;
        try
        {
            double d1 =  double.Parse(ShowArea.transform.GetChild(0).GetComponent<InputField>().text);
            i++;
            double d2 =  double.Parse(ShowArea.transform.GetChild(1).GetComponent<InputField>().text);
            i++;
            int n =  int.Parse(ShowArea.transform.GetChild(2).GetComponent<InputField>().text);
            double result = 2 * Math.Abs(d1 - d2) / n * 1e6;
            Result.text = (2 * Math.Abs(d1-d2) / n*1e6).ToString();
            Result2.text = (Math.Round(Math.Abs(GlobalVariable.WaveLength1-result) / GlobalVariable.WaveLength1*100,2)).ToString()+"%";
        }
        catch (Exception)
        {
            ShowArea.transform.GetChild(i).GetComponent<InputField>().text = "输入类型错误！！！";
        }

    }

    private void Clear()
    {
        ShowArea.transform.GetChild(0).GetComponent<InputField>().text="";
        ShowArea.transform.GetChild(1).GetComponent<InputField>().text="";
        ShowArea.transform.GetChild(2).GetComponent<InputField>().text="";
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowState == 2)
        {
            if (ShowArea.transform.GetComponent<RectTransform>().sizeDelta.y < MaxHeight)
                ShowArea.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(MinWidth, ShowArea.transform.GetComponent<RectTransform>().sizeDelta.y + Time.deltaTime * speed);
            else
                ShowState = 1;
        }else if (ShowState == -2)
        {
            if(ShowArea.transform.GetComponent<RectTransform>().sizeDelta.y > 0)
                ShowArea.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(MinWidth, ShowArea.transform.GetComponent<RectTransform>().sizeDelta.y-Time.deltaTime*speed);
            else
                ShowState = -1;
        }
    }
    void OnGUI()
    {
        if (IsEnter)
        {
            GUI.Box(new Rect(Input.mousePosition.x+15, Screen.height -Input.mousePosition.y+15, 100, 25), new GUIContent("显示计算界面"));
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        IsEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsEnter = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ShowState == 1)
        {
            ShowArea.SetActive(false);
            ShowState = -2;
        }
        else
        {
            ShowArea.SetActive(true);
            ShowArea.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(MinWidth, 0);
            ShowState = 2;
        }
    }
}
