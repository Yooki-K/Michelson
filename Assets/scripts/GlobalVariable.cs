using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Target
{
    public Vector3 Point;
    public Vector3 Direction;
    public Target(Vector3 Point, Vector3 Direction)
    {
        this.Point = Point;
        this.Direction = Direction;
    }
}

public static class GlobalVariable
{
    //public static float d1;//主尺       mm
    //public static float d2;//读数窗口   0.01mm
    //public static float d3;//微动手轮   0.00001mm
    public static float d =0;//读数           单位mm     精确度0.00001mm
    public static int MAX = 5;//最大主尺长度   单位mm
    public static double WaveLength1= 632.8;//波长1   单位nm
    public static double WaveLength2 = 632.8;//波长2   单位nm
    public static double Refractivity =1.0;//折射率

    public static int R = 10;//屏幕大小
    public static double Thickness = 2.5;//干涉厚度  单位mm
    public static double L = 30;//未知

    public static string ActiveName ="";
    public static bool IsLook = false;

    public static int[,] Node = new int[2,2];

    public static Vector3[] Dirs = new Vector3[5];
    public static Vector3[] Points = new Vector3[5];
    public static bool[] ISOK = new bool[5];



    //public static wave = 2*d/n;

}
