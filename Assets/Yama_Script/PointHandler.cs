using UnityEngine;
using System.Collections;
using UnityEngine.UI; // テキストのコンポーネントはこのままだと取得出来ないのでこれが必要

public class PointHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ポイントの設定
	public void setPoint(long point){
		
		// ポイント変更
		//(IEnumerator PointAnimationでアニメーションが付加されたポイント変更を作成したので不要になった)
//		gameObject.GetComponent<Text>().text = point.ToString();

		// アニメーションを止める
		StopCoroutine("PointAnimation");

		// アニメーションスタート
		StartCoroutine(
			PointAnimation(
				long.Parse(gameObject.GetComponent<Text>().text),
				point,
				0.2f
			)
		);

	}

	// ポイントアニメーション
	// (コルーチンは複数のフレームにまたいで実行される)
	private IEnumerator PointAnimation(long start, long end, float time){

		// アニメーション開始時間
		float startTime = TimeManager.time;

		// アニメーション終了時間
		float endTime = startTime + time;

		// 1フレームごとに数値を上昇させる
		do{
			// アニメーション中の今の経過時間を計算
			float t = (TimeManager.time - startTime) / time;

			// 数値を更新
			long updateValue = (long)( ((end - start) * t) + start );

			// テキストを更新
			GetComponent<Text>().text = updateValue.ToString();

			// 1フレーム待つ
			yield return null;

		}while(TimeManager.time < endTime);

		// 数値を最終値に合わせる
		GetComponent<Text>().text = end.ToString();

	}

}
