using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            MessageBox.Show("观看", "放下","请选择对放大镜进行的操作：");
            MessageBox.click1 = () =>
            {
                Debug.Log("click1");
            };
            MessageBox.click2 = () =>
            {
                Debug.Log("click2");
            };


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
