 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardMove : MonoBehaviour {

	//時間管理　
//	public float time;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//時間の更新
		transform.localPosition = new Vector3 (0, - TimeManager.tick * 0.05f, 0);
	}
}
