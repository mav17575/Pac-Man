using UnityEngine;
using System.Collections;

public class SpriteSplitter : MonoBehaviour {
    public Texture2D sheet;
    public int sx, sy;
	// Use this for initialization
	void Start () {
        Split();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Split()
    {
        for (int y=0;y<sheet.height;y+=sy)
        {
            for (int x = 0; x < sheet.width; x+=sx)
            {
                Texture2D t = new Texture2D(sx,sy);
                t.SetPixels(sheet.GetPixels(x, y, sx, sy));
                /*for (int y2 = 0; y2 < sy; y2 ++)
                {
                    for (int x2 = 0; x2 < sx; x2 ++)
                    {
                        t.SetPixel(x2,y2,new Color(0,0,0));
                    }
                }*/
                // Debug.Log(t.GetPixels(0, 0, sx, sy)[0]);
                Sprite s = Sprite.Create(t, new Rect(0, 0, sx, sy), new Vector2(0, 0));
                //Sprite s = Sprite.Create(sheet, new Rect(0,0, sheet.width, sheet.height), new Vector2(0, 0));
                GameObject g = new GameObject();
                g.AddComponent(typeof(SpriteRenderer));
                g.GetComponent<SpriteRenderer>().sprite = s;
                GameObject.Instantiate(g);
            }
        }
    }
}
