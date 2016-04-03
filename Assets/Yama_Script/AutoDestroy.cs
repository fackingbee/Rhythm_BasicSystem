using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}

	// アニメーション終了時のイベント
	public void OnAnimationStop(){
		// 削除
		Destroy(gameObject);
	}

	// アニメーション終了時のイベント(親を消す処理)
	public void OnAnimationStopOnParent(){
		// 削除
		Destroy(transform.parent.gameObject);

	}
}
