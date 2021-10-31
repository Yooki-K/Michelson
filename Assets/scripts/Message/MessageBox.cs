
using System;


public static class MessageBox
{
    /// <summary>
    /// true表示模态框
    /// </summary>
    //public static bool type;
    //三个委托，分别为三个按钮的点击运行事件
    public static Action click1;
    public static Action click2;
    public static Action ClickClose;
    public static int Result;
    //标题
    public static string headText;
    //内容
    public static string option1;
    public static string option2;
    //状态。用于显示或隐藏弹出框
    public static int state = -1;

    /// <summary>
    ///重试按钮点击事件
    /// </summary>
    public static void OnClick1()
    {
        state = -1;
        click1?.Invoke();
        click1 = null;
    }
    /// <summary>
    /// 取消按钮点击事件
    /// </summary>
    public static void OnClick2()
    {
        state = -1;
        click2?.Invoke();
        click2 = null;
    }


    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="_head">标题</param>
    public static void Show(string _option1, string _option2, string _head)
    {
        option1 = _option1;
        option2 = _option2;
        headText = _head;
        state = 0;
    }
}
