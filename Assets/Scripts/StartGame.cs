using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [Header("开始")] public Button btn1;
    [Header("退出")] public Button btn2;
    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(Launch);
        btn2.onClick.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch()
    {
        SceneManager.LoadScene(1);
    }
    private void Close()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
