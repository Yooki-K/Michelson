using UnityEngine;
using UnityEngine.UI;


public class Draw : MonoBehaviour
{
    

    public Texture2D texture2;
    public Text text;
    private Renderer render; 

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    //创建设置贴图文件

    void Update()
    {
        if (!GlobalVariable.IsOpenLaser|| !GlobalVariable.IsCross)
        {
            render.materials[render.materials.Length - 1].mainTexture = texture2;
            return;
        }
        int n = GlobalVariable.R * GlobalVariable.R * 4 + 1;
        Texture2D temp = new Texture2D(texture2.width, texture2.height, texture2.format, false);
        if (GlobalVariable.IsOpenLaser)
        {
            int x1 = GlobalVariable.Node[0, 0];
            int y1 = GlobalVariable.Node[0, 1];
            int x2 = GlobalVariable.Node[1, 0];
            int y2 = GlobalVariable.Node[1, 1];
            //判断准备工作是否操作好 begin
            int ToleranceScope = 15;
            var center = new Vector2(0,0);
            if(Vector2.Distance(new Vector2(x1, y1), center)<=ToleranceScope*2 &&
                Vector2.Distance(center, new Vector2(x2, y2)) < ToleranceScope*2 &&
                Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2)) <= ToleranceScope&&
                GlobalVariable.IsOn)
            {
                GlobalVariable.IsOK = true;
            }
            else
            {
                
                GlobalVariable.IsOK = false;
            }
            //end
            if (GlobalVariable.IsOK)
            {
                Chart.Draw(GlobalVariable.R, GlobalVariable.Refractivity, GlobalVariable.WaveLength1, GlobalVariable.WaveLength2, GlobalVariable.d, GlobalVariable.L);
                double min = Chart.GetMin(Chart.Points);
                double max = Chart.GetMax(Chart.Points);
                for (int i = 0; i < Chart.Points.Count-1; i++)
                {
                    for (int j = 0; j < Chart.Points[i].Count-1; j++)
                    {
                        temp.SetPixel(i+n/2, j+n/2, texture2.GetPixel(GlobalVariable.Node[0, 0] - i, GlobalVariable.Node[0, 1] - j) + Chart.GetColorMap(Chart.Points[i][j], min, max));
                    }
                }
            }
            else
            {
                var data = texture2.GetRawTextureData<Color32>();
                temp.LoadRawTextureData(data);
                for (int i = -2; i <= 2; i++)
                {
                    for (int j = -2; j <= 2; j++)
                    {

                        temp.SetPixel(x1 - i, y1 - j, Color.red);
                        temp.SetPixel(x2 - i, y2 - j, Color.red);
                    }
                }
            }
        }
        temp.Apply();
        render.materials[render.materials.Length - 1].mainTexture = temp;

    }

}
