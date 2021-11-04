using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct NP{
    public List<List<double>> x;
    public List<List<double>> y;
    public NP(int i) { 
        x = new List<List<double>>();
        y = new List<List<double>>();
    }
}
public static class Chart 
{
    public static List<List<double>> Points = new List<List<double>>();

    public static void Draw(int r, double sn, double dWave1,double dWave2,double d,double L)
    {
        Clear();
        //      单位换算，统一mm
        double Lambda1 = dWave1*1e-6; 
        double Lambda2 = dWave2*1e-6;
        L = L * 10;
        d = Math.Abs(d);
        int N = r * 40 + 1;  // N设置图像精度
        List<double> dx = Linspace(-r, r, N);
        List<double> dy = Linspace(-r, r, N);
        NP np = Meshgrid(dx, dy);// 生成坐标二维矩阵
        List<List<double>> dr = Sqrt(np); // 计算x、y对应的极坐标距离dr
        List<List<double>> R1 = Sqrt(L , dr);
        List<List<double>> R2 = Sqrt((2*d+L) , dr);
        double k1 = 2 * Math.PI / Lambda1 * sn;
        double k2 = 2 * Math.PI / Lambda2 * sn;
        List<List<double>> I1 = Cos(k1,R2 ,R1);
        List<List<double>> I2 = Cos(k2,R2 ,R1);
        Points = Calculate(I1, I2, 8);// 计算光强并归一化
    }
    private static  List<double> Linspace(double l,double r,int N)
    {
        double derda = (r - l) / (N - 1);
        List<double> result = new List<double>();
        for (int i = 0; i < N; i++)
        {
            result.Add(l + derda * i);
        }
        return result;
    }
    private static NP Meshgrid(List<double> dx, List<double> dy)
    {
        NP nP = new NP(0);     
        foreach (var y in dy)
        {
            nP.x.Add(dx);
            List<double> temp = new List<double>();
            for (int i = 0; i < dx.Count; i++)
            {
                temp.Add(y);
            }
            nP.y.Add(temp);
        }
        return nP;
    }

    private static List<List<double>> Sqrt(NP np)
    {
        List<List<double>> result = new List<List<double>>();
        for (int i = 0; i < np.x.Count; i++)
        {
            List<double> temp = new List<double>();
            for (int j = 0; j < np.x[i].Count; j++)
            {
                temp.Add(Math.Sqrt(Math.Pow(np.x[i][j],2)+Math.Pow(np.y[i][j],2)));
            }
            result.Add(temp);
        }
        return result;
    }

    private static List<List<double>> Sqrt(double x,List<List<double>> y)
    {
        List<List<double>> result = new List<List<double>>();
        for (int i = 0; i < y.Count; i++)
        {
            List<double> temp = new List<double>();
            for (int j = 0; j < y[i].Count; j++)
            {
                temp.Add(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y[i][j], 2)));
            }
            result.Add(temp);
        }
        return result;
    }

    private static List<List<double>> Cos(double k, List<List<double>> r1, List<List<double>>r2)
    {
        List<List<double>> result = new List<List<double>>();
        for (int i = 0; i < r1.Count; i++)
        {
            List<double> temp = new List<double>();
            for (int j = 0; j < r1[i].Count; j++)
            {
                temp.Add(2+2*Math.Cos(k*(r2[i][j]-r1[i][j])));
            }
            result.Add(temp);
        }
        return result;
    }

    private static List<List<double>> Calculate(List<List<double>> I1, List<List<double>>I2,int n)
    {
        List<List<double>> result = new List<List<double>>();
        for (int i = 0; i < I1.Count; i++)
        {
            List<double> temp = new List<double>();
            for (int j = 0; j < I1[i].Count; j++)
            {
                temp.Add((I1[i][j]+I2[i][j])/n);
            }
            result.Add(temp);
        }
        return result;
    }

    public static double GetMax(List<List<double>> l)
    {
        List<double> temp = new List<double>();
        for (int i = 0; i < l.Count; i++)
        {
                temp.Add(l[i].Max());
        }
        return temp.Max();
    }
    public static double GetMin(List<List<double>> l)
    {
        List<double> temp = new List<double>();
        for (int i = 0; i < l.Count; i++)
        {
            temp.Add(l[i].Min());
        }
        return temp.Min();
    }
    public static Color GetColorMap(double v,double min,double max)
    {
        float temp = (float)((v - min) / (max - min));
        return new Color(temp*0.8f, temp*0.1f, temp*0.1f,0.5f);
    }
    private static void Clear()
    {
        if (Points.Count>0)
        {
            Points.Clear();
        }
    }
}
