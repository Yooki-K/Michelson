using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class RayReflect : MonoBehaviour
{
    [Header("最大距离")] public float maxDistance = 200;

    [Header("最多入射次数")] public int maxReflectTimes = 10;

    //[Header("折射物体")] public List<GameObject> skip;
    public bool IsReflect;
    public GameObject From;
    public Image image;
    public int RayIndex;

    /// <summary>
    /// 渲染射线
    /// </summary>
    private LineRenderer _lineRender;

    /// <summary>
    /// 射到的点的集合，用来渲染射线
    /// </summary>
    private List<Vector3> _renderPoints;

    private void Awake()
    {
        _lineRender = GetComponent<LineRenderer>();
        _lineRender.startWidth = 0.01f;
        _lineRender.endWidth = 0.01f;
        _lineRender.startColor=Color.red;
        _lineRender.endColor=Color.red;
        GlobalVariable.Points[0] = transform.position;
        GlobalVariable.Points[1] = transform.position;
        GlobalVariable.Dirs[0] = transform.forward;
        GlobalVariable.Dirs[1] = transform.forward;
        GlobalVariable.IsOkList[0]= true;
        GlobalVariable.IsOkList[1]= true;
    }

    private void Update()
    {
        if (!GlobalVariable.IsOpenLaser)
        {
            _lineRender.positionCount = 0;
            return;
        }
        GlobalVariable.Points[0] = transform.position;
        GlobalVariable.Points[1] = transform.position;
        GlobalVariable.Dirs[0] = transform.forward;
        GlobalVariable.Dirs[1] = transform.forward;
        if (GlobalVariable.IsOkList[RayIndex])
        {
            _renderPoints = new List<Vector3>();
            _renderPoints.Add(GlobalVariable.Points[RayIndex]); //LineRenderer以自己为起点

            _renderPoints.AddRange(GetRenderPoints(GlobalVariable.Points[RayIndex], GlobalVariable.Dirs[RayIndex],
                maxDistance, maxReflectTimes));//获取反射点
            if (GlobalVariable.isShowPath)
            {
                _lineRender.positionCount = _renderPoints.Count;
                _lineRender.SetPositions(_renderPoints.ToArray());
            }
            else
            {
                _lineRender.positionCount = 0;
            }

        }

    }


        /// <summary>
        /// 计算折射角度
        /// </summary>
        /// <param name="I">入射光方向</param>
        /// <param name="N">平面法线方向</param>
        /// <param name="h">折射率</param>
        /// <returns></returns>
    public Vector3 Refract(Vector3 I, Vector3 N, float h = 1.0f / 1.5f)
    {
        I = I.normalized;//单位化
        N = N.normalized;

        float A = Vector3.Dot(I, N);
        float B = 1.0f - h * h * (1.0f - A * A);
        Vector3 T = h * I - (h * A + Mathf.Sqrt(B)) * N;
        if (B > 0)
            return T;
        else
            return Vector3.zero;
    }


        /// <summary>
        /// 获得反射点
        /// </summary>
        /// <param name="start">起始位置</param>
        /// <param name="dir">方向</param>
        /// <param name="dis">最大距离</param>
        /// <param name="times">反射次数</param>
      private List<Vector3> GetRenderPoints(Vector3 start, Vector3 dir, float dis, int times)
      {
        var hitPosList = new List<Vector3>();
        bool change = true;
        while (dis > 0 && times > 0)
        {
            if (!Physics.Raycast(start, dir, out RaycastHit hit, dis))
                break;
            
            GameObject target = hit.collider.gameObject;
            if (target.name== "BeamExpander")
            {
                hitPosList.Add(hit.point);
                break;
            }
            if (target.name.Contains("Maoboli"))
            {
                var renderer = hit.collider.GetComponent<Renderer>();
                Texture2D texture = renderer.materials[renderer.materials.Length - 1].mainTexture as Texture2D;
                //现在在所碰到的物体上绘制一个像素
                int n = GlobalVariable.R * GlobalVariable.R * 4 + 1;
                var pixelUV = hit.textureCoord;
                if (RayIndex==2)
                {
                    GlobalVariable.Node[0,0] = (int)(pixelUV.x * texture.width);
                    GlobalVariable.Node[0,1] = (int)(pixelUV.y * texture.height);
                }
                else if(RayIndex==4)
                {
                    GlobalVariable.Node[1, 0] = (int)(pixelUV.x * texture.width);
                    GlobalVariable.Node[1, 1] = (int)(pixelUV.y * texture.height);
                }
                hitPosList.Add(hit.point);
                break;
            }
            if (!IsReflect)//折射
            {
                hitPosList.Add(hit.point);
                start = hit.point;
                if(change)
                {
                    dir = Refract(dir, hit.normal,1.0f/1.5f);
                    change = !change;
                }
                else
                {
                    dir = Refract(dir, hit.normal, 1.5f / 1.0f);
                    change = !change;
                }
                dis -= (hit.point - From.transform.position).magnitude;
            }
            else//反射
            {
                hitPosList.Add(hit.point);
                var reflectDir = Vector3.Reflect(dir, hit.normal);
                dis -= (hit.point - From.transform.position).magnitude;
                start = hit.point;
                dir = reflectDir;
            }
            times--;
        }
        switch (RayIndex+1)
        {
            case 1:
                GlobalVariable.Dirs[2] = dir;
                GlobalVariable.Points[2] = start;
                GlobalVariable.IsOkList[2] = true;
                break;
            case 2:
                GlobalVariable.Dirs[3] = -dir;
                GlobalVariable.Points[3] = start;
                GlobalVariable.IsOkList[3] = true;
                break;
            case 4:
                GlobalVariable.Dirs[4] = dir;
                GlobalVariable.Points[4] = start;
                GlobalVariable.IsOkList[4] = true;
                break;
        }

        return hitPosList;
      }

}