using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float dx = Input.GetAxis("Mouse X");
        float dy = Input.GetAxis("Mouse Y");


        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //Debug.Log(hitInfo.transform.name);
            }
        }
        // 旋转 鼠标右键
        if (Input.GetMouseButton(1)) 
        {
            if (Mathf.Abs(dx) > 0 || Mathf.Abs(dy) > 0)
            {
                // 获取摄像机欧拉角
                Vector3 angles = transform.rotation.eulerAngles;
                
                // 欧拉角表示按照坐标顺序旋转，比如angles.x=30，表示按x轴旋转30°，dy改变引起x轴的变化
                angles.x = Mathf.Repeat(angles.x + 180f, 360f) - 180f;
                angles.y += dx;
                angles.x -= dy;
                // 设置摄像头旋转
                Quaternion rotation = Quaternion.identity;
                rotation.eulerAngles = new Vector3(0, angles.y, 0);
                transform.rotation = rotation;
            }
        }
        //移动
        //空格键抬升高度
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed  * Time.deltaTime, transform.position.z);
        }
        //shift键降低高度
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed  * Time.deltaTime, transform.position.z);
        }
        //w键前进
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        //s键后退
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        //a键后退
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        //d键后退
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }
}

