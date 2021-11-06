using UnityEngine;
using UnityEngine.UI;


public class Draw : MonoBehaviour
{
    
    //保存贴图
    public Image targetMaterial;
    public bool IsOK = false;

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
        int n = 401;
        Texture2D texture = new Texture2D(n, n);
        if (IsOK)
        {
            Chart.Draw(GlobalVariable.R, GlobalVariable.Refractivity, GlobalVariable.WaveLength1, GlobalVariable.WaveLength2, GlobalVariable.d, GlobalVariable.L);
            
            
            double min = Chart.GetMin(Chart.Points);
            double max = Chart.GetMax(Chart.Points);
            for (int i = 0; i < Chart.Points.Count; i++)
            {
            
                for (int j = 0; j < Chart.Points[i].Count; j++)
                {
                    texture.SetPixel(i, j, Chart.GetColorMap(Chart.Points[i][j],min,max) );
                }
            }
        }
        else
        {
            Debug.Log(GlobalVariable.Node[0, 0]+" 0 "+ GlobalVariable.Node[0, 1]);
            for (int i = -25; i <= 25; i++)
            {
                for (int j = -25; j <= 25; j++)
                {
                    texture.SetPixel(GlobalVariable.Node[0,0]-i, GlobalVariable.Node[0,1]-j, Color.red);//画激光
                    texture.SetPixel(GlobalVariable.Node[1,0]-i, GlobalVariable.Node[1,1]-j, Color.red);//画激光
                }
            }
        }

        //应用贴图
        texture.Apply();
        //将贴图数据赋值给Image的sprite
        targetMaterial.sprite = Sprite.Create(texture, new Rect(0, 0, n, n), Vector2.zero); ;

    }

}
