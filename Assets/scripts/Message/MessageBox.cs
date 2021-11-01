
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public static class MessageBox
{
    //状态。用于显示或隐藏弹出框
    public static int state = -1;
    public static string[] agrs = null;
    public static List<UnityAction> actions = new List<UnityAction>();
    
    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="_agrs">第一个为内容，其余为选项</param>
    public static void Show(string [] _args)
    {
        actions.Clear();
        agrs = _args;
        state = 0;
    }
}
