using UnityEngine;
using UnityEngine.UI;


public class Draw : MonoBehaviour
{
    
    //保存贴图
    public Image targetMaterial;

    void Start()
    {
        targetMaterial.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(401,401);
    }

    //创建设置贴图文件

    void Update()
    {
        if (!GlobalVariable.IsLook) return;
        if (Input.GetKey(KeyCode.Escape))
        {
            GlobalVariable.IsLook = false;
            targetMaterial.gameObject.SetActive(false);
            return;
        }
        if(!targetMaterial.gameObject.activeSelf)
            targetMaterial.gameObject.SetActive(true);
        Chart.Draw(GlobalVariable.R, GlobalVariable.Refractivity, GlobalVariable.WaveLength1, GlobalVariable.WaveLength2, GlobalVariable.d, GlobalVariable.L);
        //Chart.Draw(GlobalVariable.R, GlobalVariable.Refractivity, GlobalVariable.WaveLength1, GlobalVariable.WaveLength2, 2.5, GlobalVariable.L);
        int n = Chart.Points.Count;
        Texture2D texture = new Texture2D(n, n);
        double min = Chart.GetMin(Chart.Points);
        double max = Chart.GetMax(Chart.Points);
        //Debug.Log("min "+min);
        //Debug.Log("max "+max);
        for (int i = 0; i < Chart.Points.Count; i++)
        {
            
            for (int j = 0; j < Chart.Points[i].Count; j++)
            {
                texture.SetPixel(i, j, Chart.GetColorMap(Chart.Points[i][j],min,max) );
            }
        }
        //应用贴图
        texture.Apply();
        //将贴图数据赋值给Image的sprite
        targetMaterial.sprite = Sprite.Create(texture, new Rect(0, 0, n, n), Vector2.zero); ;

    }

}
