using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// オーディオを再生
		gameObject.GetComponent<AudioSource>().PlayDelayed(1.59f);
	}

	// Update is called once per frame
	void Update () {

	}
}