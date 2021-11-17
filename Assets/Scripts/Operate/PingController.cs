using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PingController : MonoBehaviour
{

    //click部分
    //begin
    public bool IsDown = false;
    public Camera MainCamera;
    public Camera LookCamera;

    public void OnMouseDown()
    {
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
            if (MessageBox.state == -1)
            {
                if(transform.parent.parent.parent.localEulerAngles == new Vector3(0, 180, 0))
                {
                    string s;
                    if (GlobalVariable.IsLook)
                    {
                        s = "关闭分屏观看";
                    }
                    else
                    {
                        s = "开启分屏观看";
                    }
                    MessageBox.Show(new string[] { "请选择对放大镜进行的操作(ESC键退出)：", s, "逆时针旋转90°", "顺时针旋转90°" });

                    MessageBox.actions.Add(() =>
                    {
                        GlobalVariable.IsLook = !GlobalVariable.IsLook;
                        Look(GlobalVariable.IsLook);
                        Debug.Log(s);
                        MessageBox.state = -1;
     
                    });
                }
                else
                {
                    MessageBox.Show(new string[] { "请选择对放大镜进行的操作(ESC键退出)：", "逆时针旋转90°", "顺时针旋转90°" });
                }

                MessageBox.actions.Add(() =>
                {
                    Debug.Log("逆时针旋转90°");
                    transform.parent.parent.parent.Rotate(0,0,-90);
                    MessageBox.state = -1;

                });
                MessageBox.actions.Add(() =>
                {
                    Debug.Log("顺时针旋转90°");
                    transform.parent.parent.parent.Rotate(0, 0, 90);
                    MessageBox.state = -1;

                });
            }
            IsDown = false;
        }


    }
    //end

    private void Look(bool IsLook)
    {
        if (IsLook)
        {
            MainCamera.rect = new Rect(0,0,0.6f,1);
            LookCamera.rect = new Rect(0.6f,0,0.4f,1);
        }
        else
        {
            MainCamera.rect = new Rect(0, 0, 1, 1);
            LookCamera.rect = new Rect(0, 0,0, 1);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
