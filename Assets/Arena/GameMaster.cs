using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public Field field;
    public GameObject Pacman;
	// Use this for initialization
	void Start () {
        field.NewField();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
