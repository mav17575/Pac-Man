using UnityEngine;
using System.Collections;
using System.Linq;

public class Field : MonoBehaviour {

    public int TWidth;
    public int THeight;
    public int[] field;
    public GameObject[] Sprites;
    public GameObject[] Visualf;
    public Vector3 Size = Vector3.zero;
    private float wh;
    private float hh;
    private float wr;
    private float hr;
    private System.Random rand = new System.Random();
    void Start () {
        System.Array.Sort(Sprites, (x, y) => {
            int sx = x.GetComponent<SpriteValue>().type;
            int sy = y.GetComponent<SpriteValue>().type;
            return sx<sy ? -1:1;
        });

        NewField();
    }
	
	// Update is called once per frame
	void Update () {
        float height = transform.localScale.y;
        float width = transform.localScale.x;

        float w = Screen.width / width;
        float h = Screen.height / height;

        float ratio = (w / h);

        float size = (height / 2);

        if (w < h)
            size /= ratio;

        Camera.main.orthographicSize = size*1.1f;
        //print(ConvVecToGrid(Visualf[0].transform.localPosition));
    }

    public Vector3 ConvGridToVec(float x, float y)
    {
        float vx = (x - wh + transform.localPosition.x) *wr;
        float vy = (-y + hh + transform.localPosition.y)*hr;
        Vector3 v = new Vector3(vx+wr/2  , vy-hr/2 , 0);
        return v;
    }

    public Vector3 ConvVecToGrid(Vector2 vec)
    {
        return new Vector3((vec.x+transform.localScale.x/2)/Size.x , (-vec.y + transform.localScale.y/2) / Size.y ,0);
    }

    public bool InField(Vector3 vec)
    {
        Vector3 vc = ConvVecToGrid(vec);
        bool greaterz = Mathf.Min(vc.x, vc.y) >= 0;
        if (!greaterz||(int)vc.x>TWidth-1.5f||(int)vc.y>THeight-1.5f) return false;

        return true;
    }

    public bool IsValid(Vector3 vec)
    {
        Vector3 vc = ConvVecToGrid(vec);
        bool greaterz = Mathf.Min(vc.x, vc.y) >= 0;
        if (!greaterz || (int)vc.x > TWidth-1.5f || (int)vc.y > THeight-1.5f) return false;

        return field[(int)vc.x+(int)vc.y*TWidth]==0;
    }

    public void NewField()
    {
        //DestroyChildren();
        DestroyObjects(Visualf);
        SpriteRenderer b = Sprites[0].GetComponent<SpriteRenderer>();
        Vector3 size = b.bounds.size;
        float px = b.sprite.pixelsPerUnit;
        field = FieldGenerator.GenerateField(rand.Next(-1000, 1000), TWidth, THeight);
        float xr = size.x / px;
        float yr = size.y / px;
        Size = new Vector3(xr,yr,0);
        transform.localScale = new Vector3(TWidth * xr, THeight * yr, 1);
        wh = (TWidth / 2.0f);
        hh = (THeight / 2.0f);
        wr = (transform.localScale.x / TWidth);
        hr = (transform.localScale.y / THeight);
        Visualf = fillTiles(Sprites, field, TWidth, THeight,Size, transform);
        // transform.localScale = new Vector3(TWidth * xr-(xr/2), THeight * yr-(yr/2), 1);
    }

    public static GameObject[] fillTiles(GameObject[] sprites, int[] field, int TWidth, int THeight, Vector3 size, Transform transform)
    {
        GameObject[] visualf = new GameObject[TWidth*THeight];
        for (int y = 0; y < THeight; y++)
        {
            for (int x = 0; x < TWidth; x++)
            {
                int i = (int)(x + y * TWidth);
                float wr = (transform.localScale.x / TWidth);
                float hr = (transform.localScale.y / THeight);
                // print((TWidth / 2) * (transform.localScale.x / TWidth));
                GameObject g = (GameObject)Instantiate(sprites[evaluateTile(field,(int)x,(int)y,TWidth,THeight)], new Vector3( (x - (TWidth / 2.0f)) * wr + (size.x/2), (-y + (THeight / 2.0f)) * hr -(size.y/2), 0), Quaternion.identity);
                // g.transform.parent = transform;
                visualf[i] = g;
            }
        }
        return visualf;
    }

    public static int evaluateTile(int[] field,int x, int y, int TWidth, int THeight)
    {
        if (field[x + y * TWidth] == 0) return 0;
        int yui = y-1;
        int yu = yui>=0 ? yui*TWidth+x: -1;

        int yli = y + 1;
        int yl = yli < THeight ? yli*TWidth + x : -1;

        int xri = x + 1;
        int xr = xri < TWidth ? xri+(y*TWidth) : -1;

        int xli = x - 1;
        int xl = xli >= 0 ? xli + (y * TWidth) : -1;

        int yuv = yu > -1 ? field[yu] : 0;
        int xrv = xr > -1 ? field[xr] : 0;
        int ylv = yl > -1 ? field[yl] : 0;
        int xlv = xl > -1 ? field[xl] : 0;

        //string s = yuv + "" + xrv + "" + ylv + "" + xlv;
        int v = xlv + (ylv * 2) + (xrv * 2 * 2) + (yuv * 2 * 2 * 2);
        //print(v);
        return v;
    }

    public static void DestroyObjects(GameObject[] go)
    {
        foreach (GameObject g in go)
        {
            GameObject.Destroy(g);
        }
    }

    public void DestroyChildren()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static void DestroyChildren(GameObject go)
    {
        foreach (Transform child in go.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static SpriteValue[] svalues(GameObject[] sprites)
    {
        SpriteValue[] sv = new SpriteValue[sprites.Length];
        for(int i =0; i<sv.Length;i++)
        {
            sv[i] = sprites[i].GetComponent<SpriteValue>();
        }
        return sv;
    }
}
