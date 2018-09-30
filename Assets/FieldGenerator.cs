using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public class FieldGenerator{
    private static System.Random Rand = new System.Random();
    public static int[] GenerateField(int seed, int twidth, int theight, int gy, int gw, int gh)
    {
        int[] field = new int[twidth * theight];
        Random.InitState(seed);
        int hwidth = twidth / 2;
        string maze = run_cmd(Application.dataPath+"/Arena/MazeGenerator.py", (hwidth+1)+" "+theight+" "+gy+" "+(gw/2)+" "+gh);
        maze = Regex.Replace(maze, @"\s+", "");
        for (int y=0;y<theight;y++)
        {
            for (int x = 0; x < twidth; x++)
            {
                bool b = (x == 0 || x == twidth - 1) || (y == 0 || y == theight - 1);
                int i = x + y * twidth;
                field[i] = maze[i] == '.' ? 0:1;
                //field[x + y * twidth] = b?1:PerlinNoise(x,y,2.5f,0.48f);
            }
        }
        return field;
    }

    private static string run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/Users/Granit/AppData/Local/Programs/Python/Python37/python.exe";
        start.Arguments = string.Format("{0} {1}", cmd, args);
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.CreateNoWindow = true;
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }

    public static int NormalRandom(int l, int u)
    {
        return Random.Range(l,u);
    }

    public static int PerlinNoise(int x, int y, float ratio,float threshold)
    {
    float min = float.MaxValue;
    float max = float.MinValue;
    float p = Mathf.PerlinNoise(x * ratio, y * ratio);

    if (p < min) min = p;
    if (p > max) max = p;

    if (p > threshold)
    {
        return 1;
    }
    else
        {
        return 0;
        }
    }

}
