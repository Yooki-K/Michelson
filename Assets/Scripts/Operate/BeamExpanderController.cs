using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamExpanderController : MonoBehaviour
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
                MessageBox.Show(new string[] { "请选择对扩束镜进行的操作(ESC键退出)：", "归位", "摆放"});

                MessageBox.actions.Add(() =>
                {
                    Debug.Log("归位");
                    transform.position = new Vector3(0.32f, 0, 0);
                    transform.LookAt(Vector3.left);
                    GlobalVariable.IsOn = false;
                    MessageBox.state = -1;

                });
                MessageBox.actions.Add(() =>
                {
                    Debug.Log("摆放");
                    transform.position = new Vector3(0, 0, 0);
                    transform.LookAt(Vector3.forward);
                    GlobalVariable.IsOn = true;
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
