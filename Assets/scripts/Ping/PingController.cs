using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PingController : MonoBehaviour
{

    //click部分
    //begin
    public bool IsDown = false;


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
                    MessageBox.Show(new string[] { "请选择对放大镜进行的操作(ESC键退出)：", "观看", "逆时针旋转90°", "顺时针旋转90°" });

                    MessageBox.actions.Add(() =>
                    {
                        GlobalVariable.IsLook = true;
                        Debug.Log("观看");
                        Debug.Log(transform.parent.parent.parent.localEulerAngles);
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


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
