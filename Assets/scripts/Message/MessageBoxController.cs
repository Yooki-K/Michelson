using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageBoxController : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Text HeadTest;
    public Text option1;
    public Text option2;

    private void init()
    {
        button1.onClick.AddListener(MessageBox.OnClick1);
        button2.onClick.AddListener(MessageBox.OnClick2);
        option1.text = MessageBox.option1;
        option2.text = MessageBox.option2;
        HeadTest.text = MessageBox.headText;
        MessageBox.state = 1;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MessageBox.state = -1;
        }
        if (MessageBox.state == 1) return;
        if (MessageBox.state == 0) { init(); return;}
        transform.GetChild(0).gameObject.SetActive(false);
    }


}
