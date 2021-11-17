using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class  KnobController: MonoBehaviour
{
    public float SPEED = 0.5f;
    private float angle = 0f;
    public Text txt;
    public DateTime LastTime;
    public bool IsTextActive = false;
    private String ObjectName;

    public Transform M;


    //click部分
    //begin

    public bool IsDown = false;
    public Color color;
    public Color ActiveColor = Color.red;

    public void OnMouseDown()
    {
        if (MessageBox.state == -1)
            this.IsDown = true;
    }

    public void OnMouseUp()
    {
        if (IsDown)
        {
            if (GlobalVariable.ActiveName != gameObject.name)
            {
                GlobalVariable.ActiveName = gameObject.name;
            }
            else
            {
                GlobalVariable.ActiveName = "";
            }
            IsDown = false;
        }
    }
    //end

    public void JudgeIsArrival()
    {
        if (txt.text == "" || LastTime == null || !IsTextActive) return;
        TimeSpan ts = DateTime.Now - LastTime;
        if (Convert.ToInt64(ts.TotalSeconds) > 10)
        {
            txt.text = "";
            IsTextActive = false;
        }
        return;
    }

    public void SetText(String text)
    {
        txt.text = text;
        LastTime = DateTime.Now;
        IsTextActive = true;
        Debug.Log(text);
    }

    void Start()
    {
        color = GetComponent<MeshRenderer>().material.color;
        switch (gameObject.name)
        {
            case "luoding_M1_1":
                ObjectName = "上方螺丝";
                break;
            case "luoding_M1_2":
                ObjectName = "右侧螺丝";
                break;
            case "luoding_M1_3":
                ObjectName = "左侧螺丝";
                break;
            default:
                ObjectName = gameObject.name;
                break;
        }
        M = transform.parent.Find("M1");
        if (M == null)
        {
            M = transform.parent.Find("M2");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GlobalVariable.ActiveName == gameObject.name)
        {
            if (GetComponent<MeshRenderer>().material.color != ActiveColor)
            {
                GetComponent<MeshRenderer>().material.color = ActiveColor;
            }
            float speed = SPEED;
            if (Input.GetKey(KeyCode.Q)|| Input.GetKey(KeyCode.E))
            {
                if(Input.GetKey(KeyCode.Q))
                    speed = -SPEED;
                if (angle + speed * Time.deltaTime < -2.5 || angle + speed * Time.deltaTime > 2.2) return;
                angle += speed * Time.deltaTime;
                transform.Rotate(new Vector3(0,90, 0),speed * Time.deltaTime);
                transform.Translate(new Vector3(0,- speed * Time.deltaTime* 0.001f, 0));
                switch (gameObject.name.Substring(gameObject.name.Length-1))
                {
                    case "1"://上方螺丝
                        M.Rotate(new Vector3(0,0, speed * Time.deltaTime));
                        break;
                    case "2"://右侧螺丝
                        if(M.name=="M2")
                            M.Rotate(new Vector3(0, -speed * Time.deltaTime, 0));
                        else
                         M.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
                        break;
                    case "3"://左侧螺丝
                        if(M.name=="M2")
                            M.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
                        else
                            M.Rotate(new Vector3(0,- speed * Time.deltaTime, 0));
                        break;
                    default:
                        break;
                }
                if (GlobalVariable.IsShowTip)
                {
                    SetText(ObjectName + "角度为：" + angle.ToString());
                }
                


            }
        }
        else
        {
            if (GetComponent<MeshRenderer>().material.color == ActiveColor)
            {
                GetComponent<MeshRenderer>().material.color = color;
            }
        }
        JudgeIsArrival();

    }






}

