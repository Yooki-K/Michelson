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
    private String ObjectName;

    public GameObject Ruler;//主尺   Z 0.8-1.4                                                                                                                              
    public GameObject Reading;//读数窗口
    public GameObject InchingHandWheel;//微动手轮
    public GameObject RoughHandWheel;//粗动手轮
    


    //click部分
    //begin

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
            if (GlobalVariable.ActiveName != gameObject.name)
            {
                GlobalVariable.ActiveName = gameObject.name;
                SetText(ObjectName + "角度为：" + angle.ToString());
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
        if (GlobalVariable.ActiveName == gameObject.name)
        {
            if (GetComponent<MeshRenderer>().material.color != ActiveColor)
            {
                GetComponent<MeshRenderer>().material.color = ActiveColor;
            }
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
                    Ruler.transform.Translate(-Vector3.forward * speed * Time.deltaTime * I * (1.4f - 0.8f) / GlobalVariable.MAX/2);
                    Reading.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * I * 360));
                    if (gameObject.name.Contains("small"))
                    {
                        InchingHandWheel.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * I * 360 / 0.01f));
                    }
                    RoughHandWheel.transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime * I * 360));
                    SetText(ObjectName + "角度为：" + angle.ToString()+ "    M2读数为：" + GlobalVariable.d.ToString());
                } 

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
                    Ruler.transform.Translate(Vector3.forward * speed * Time.deltaTime * I * (1.4f - 0.8f) / GlobalVariable.MAX / 2);
                    Reading.transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime * I * 360));
                    if (gameObject.name.Contains("small"))
                    {
                         InchingHandWheel.transform.Rotate(new Vector3(0,0,speed * Time.deltaTime * I * 360 / 0.01f));
                    }
                    RoughHandWheel.transform.Rotate(new Vector3(0,0,speed * Time.deltaTime * I * 360));
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
        else
        {
            if(GetComponent<MeshRenderer>().material.color == ActiveColor)
            {
                GetComponent<MeshRenderer>().material.color = color;
            }
        }
        

    }






}
