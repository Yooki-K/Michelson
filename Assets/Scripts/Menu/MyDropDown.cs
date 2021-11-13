using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 将下拉菜单上的 Dropdown组件移除，替换为该脚本
/// </summary>
public class MyDropDown : Dropdown
{
    public bool AlwaysCallback = false;//是否开启 点击选项按钮总是回调
    public bool IsKeep = false;//
    
    public int SelectIndexBitMark = 0;// 当前筛选的技能质量标记

    public new void Show()
    {
        base.Show();
        Transform toggleRoot = transform.Find("Dropdown List/Viewport/Content");
        Toggle[] toggleList = toggleRoot.GetComponentsInChildren<Toggle>(false);
        for (int i = 0; i < toggleList.Length; i++)
        {
            Toggle temp = toggleList[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = ((1 << i) & SelectIndexBitMark) > 0; // 改造后
            temp.onValueChanged.AddListener(x => OnSelectItemEx(temp));
        }
    }

    public void OnSelectItemEx(Toggle toggle)
    {

        if (!toggle.isOn)
        {
            if (toggle.name.Contains("显示光路"))//撤销
            {
                GlobalVariable.isShowPath = false;
            }
            Debug.Log("撤销显示光路");
            IsKeep = true;
            toggle.isOn = true;
            return;
        }
        if (!IsKeep )//选中
        {
            if (toggle.name.Contains(""))
            {
                GlobalVariable.isShowPath = true;
            }
            Debug.Log("选中显示光路");
        }
        IsKeep = false;
        int selectedIndex = -1;
        Transform tr = toggle.transform;
        Transform parent = tr.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == tr)
            {
                selectedIndex = i - 1;
                break;
            }
        }

        if (selectedIndex < 0)
            return;
        if (value == selectedIndex && AlwaysCallback)
            onValueChanged.Invoke(value);
        else
            value = selectedIndex;
        SelectIndexBitMark ^= 1 << value;   // 新增
        Hide();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Show();
    }
}