using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 粗准焦螺旋 360°1mm
/// 细准焦螺旋 360°0.01mm
/// </summary>
public class SpainController : MonoBehaviour
{
    public float SPEED = 5;//5度每秒
    private float angle = 0f;
    private float I;
    public Text txt;
    public DateTime LastTime ;
    public bool IsTextActive = false;
    public GameObject M1;//主尺   Z 0.8-1.4                                                                                                                              
    public GameObject M2;//读数窗口
    public GameObject M3;//微动手轮
    public GameObject M4;//粗动手轮
    private String ObjectName;


    //click部分
    //begin
    public bool IsClick = false;
    public bool IsDown = false;
    public Color color;
    public Color ActiveColor = Color.red;

    public void OnMouseDown()
    {
        if(MessageBox.state==-1)
            this.IsDown = true;
    }

    public void OnMouseUp()
    {
        if (IsDown)
        {
            if (GetComponent<MeshRenderer>().material.color == color)
            {
                GetComponent<MeshRenderer>().material.color = ActiveColor;
                IsClick = true;
                SetText(ObjectName + "角度为：" + angle.ToString());
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = color;
                IsClick = false;
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
            case "Spain_large":
                ObjectName = "粗螺旋";
                I = 1f/360;
                break;
            case "Spain_small":
                ObjectName = "细螺旋";
                I = 0.01f/360;
                break;
            default:
                ObjectName = gameObject.name;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        JudgeIsArrival();
        if (IsClick)
        {
            float speed = SPEED;
            if (Input.GetKey(KeyCode.Q))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = SPEED * 10;
                }
                if(GlobalVariable.d - speed * Time.deltaTime  * I > 0)
                {
                    angle -= speed * Time.deltaTime;
                    GlobalVariable.d -= speed * Time.deltaTime  * I;
                    M1.transform.Translate(-Vector3.forward * speed * Time.deltaTime * I * (1.4f - 0.8f) / GlobalVariable.MAX/2);
                    M2.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * I * 360));
                    if (gameObject.name.Contains("small"))
                    {
                        M3.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * I * 360 / 0.01f));
                    }
                    M4.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * I * 360));
                    SetText(ObjectName + "角度为：" + angle.ToString()+ "    M2读数为：" + GlobalVariable.d.ToString());
                } 

                //2.5  1.1
            }

            if (Input.GetKey(KeyCode.E))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = SPEED * 10;
                }
                if (GlobalVariable.d + speed * Time.deltaTime  * I < GlobalVariable.MAX)
                {
                    angle += speed * Time.deltaTime;
                    GlobalVariable.d += speed * Time.deltaTime  * I;
                    M1.transform.Translate(Vector3.forward * speed * Time.deltaTime * I * (1.4f - 0.8f) / GlobalVariable.MAX / 2);
                    M2.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime * I * 360));
                    if (gameObject.name.Contains("small"))
                    {
                         M3.transform.Rotate(new Vector3(0,0,speed * Time.deltaTime * I * 360 / 0.01f));
                    }
                    M4.transform.Rotate(new Vector3(0,0,speed * Time.deltaTime * I * 360));
                    SetText(ObjectName + "角度为：" + angle.ToString()+"    M2读数为：" + GlobalVariable.d.ToString());
                }

            }
            //if (Input.GetAxis("Mouse ScrollWheel") != 0)
            //{

            //    //鼠标滚动滑轮 值就会变化
            //    if (Input.GetAxis("Mouse ScrollWheel") < 0)
            //    {
            //        Debug.Log("-1");
            //        this.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
            //    }
            //    if (Input.GetAxis("Mouse ScrollWheel") > 0)
            //    {
            //        Debug.Log("1");
            //        this.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
            //    }
            //}
        }
        

    }






}
