using UnityEngine;
using System.Collections;

public class SpriteValue : MonoBehaviour {
    //Type ist eine binäre Nummer zb 0b1001
    public string stype;
    public int type;
	// Use this for initialization
	void Start () {
       // type = StringToBin(stype);
	}

    public static int StringToBin(string bin)
    {
      return System.Convert.ToInt32(bin, 2);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
