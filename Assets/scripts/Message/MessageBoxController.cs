using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageBoxController : MonoBehaviour
{
 
    public Text HeadTest;

    public List<Button> BtnList;

    private void Init()
    {
        int i = 0;
        foreach (var x in MessageBox.agrs)
        {
            if (i == 0)
            {
                HeadTest.text = x;
            }else{
               AddButton(i, x);
            }
            i++;
        }
        MessageBox.state = 1;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        BtnList = new List<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MessageBox.state = -1;
        }
        if (MessageBox.state == 1) return;
        if (MessageBox.state == 0) {Init(); return;}
        transform.GetChild(0).gameObject.SetActive(false);
        RemoveAllChildrenButton();
    }
    public void RemoveAllChildrenButton()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Transform child =transform.GetChild(0).GetChild(i);
            if (child.GetComponent<Button>() != null)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="i">index</param>
    /// <param name="content">按钮文本</param>
    private void AddButton(int i,string content)
    {
        GameObject button = new GameObject("Option"+i, typeof(Button), typeof(RectTransform), typeof(Image)); //创建一个GameObject 加入Button组件
        GameObject text = new GameObject("Text", typeof(Text)); //创建一个GameObject 加入Text组件
        text.transform.SetParent(button.transform);
        button.transform.SetParent(transform.GetChild(0));
        button.GetComponent<RectTransform>().anchoredPosition= new Vector2(0,-40*(i-1));
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        text.GetComponent<Text>().text = content;
        text.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
        text.GetComponent<Text>().font = Font.CreateDynamicFontFromOSFont("Arial", 14);//文本字体
        text.GetComponent<Text>().color = Color.black;//文本颜色
        text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;//文本居中
        button.GetComponent<Button>().onClick.AddListener(MessageBox.actions[i-1]);
        BtnList.Add(button.GetComponent<Button>());
    }
}
