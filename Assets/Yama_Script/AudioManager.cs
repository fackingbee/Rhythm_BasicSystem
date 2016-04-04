using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

//	public static AudioSource hoge;

	// Use this for initialization
	void Start () {
		
		// オーディオを再生
		gameObject.GetComponent<AudioSource>().PlayDelayed(1.59f);

		// 先生の再生方法
//		hoge = gameObject.GetComponent<AudioSource>();
//		AudioManager.AudioPlay ();
	}

	// Update is called once per frame
	void Update () {

	}

//	public static void AudioPlay(){
//		hoge.PlayDelayed (1.59f);
//	}

}