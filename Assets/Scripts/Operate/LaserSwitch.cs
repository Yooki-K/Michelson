using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour
{
    //click部分
    //begin
    public bool IsDown = false;
    public float speed = 0.1f;


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
                string s;
                if (GlobalVariable.IsOpenLaser) { s = "关闭激光"; }
                else
                {
                    s = "打开激光";
                }
                MessageBox.Show(new string[] { "请选择对镭射激光进行的操作(ESC键退出)：", s, "激光上移(按shift加速)", "激光下移(按shift加速)"});

                MessageBox.actions.Add(() =>
                {
                    Debug.Log(s);
                    GlobalVariable.IsOpenLaser = !GlobalVariable.IsOpenLaser;
                    MessageBox.state = -1;

                });
                MessageBox.actions.Add(() =>
                {
                    Debug.Log("激光上移");  
                    transform.Rotate(new Vector3(-speed, 0, 0));
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        transform.Rotate(new Vector3(-speed*2, 0, 0));
                    }
                    else
                    {
                        transform.Rotate(new Vector3(-speed, 0, 0));
                    }

                });
                MessageBox.actions.Add(() =>
                {
                    Debug.Log("激光下移");
                    
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        transform.Rotate(new Vector3(-speed * 2, 0, 0));
                    }
                    else
                    {
                        transform.Rotate(new Vector3(speed, 0, 0));
                    }

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
