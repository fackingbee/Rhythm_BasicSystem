using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

	// TouchRingプレハブ
	public GameObject touchRingPrefab;

	// ポイントテキストプレハブ
	public GameObject pointText;

	// 各ポイントのスプライト用変数
	public enum PointTextKey{
		Miss, Bad, Good, Great, Perfect
	}

	// 各ポイント評価用のテキストスプライト 
	public Sprite[] textSprite;

	// ポイントハンドラ（PointHandlerのセット)
	public PointHandler pointHandler;

	// ゲージハンドラー
	public GageHandler gageHandler;


	// Use this for initialization
	void Start () {

		// 生成されてから削除されるまでのTick
		int tick = 79000;

		Invoke (
			"AutoDestroy",
			(60 * tick) / (TimeManager.tempo * 9600f)
		); 

		// PointHandlerのセット
		pointHandler = FindObjectOfType<PointHandler>();

		// GageHandlerのセット
		gageHandler = FindObjectOfType<GageHandler>();

	}
	
	// Update is called once per frame
	void Update () {

		//表示順の移動
		//transform.SetSiblingIndex( (int)transform.localPosition.y);
	
	}

	// ゲームオブジェクトを削除
	public void OnScoreClick(){

		// Board外の位置を計算
		// GetComponentInParentは親のコンポーネントを取得する
		Vector3 PositionInGame =
			GetComponentInParent<BoardMove> ().transform.localPosition + transform.localPosition;

		// 距離を計算
		int Distance = (int)Mathf.Abs(PositionInGame.y - (-370));

		// ポイントを計算し正規化
		float distancePoint = (410 - Distance) / 100f;

		// ポイントが0以下の時はポイントを0にする
		if(distancePoint < 0){
			distancePoint = 0;
		}

		// ポイントを加算
		GameDate.score += (int)(distancePoint * 1000);

		// ゲージを加算
		GameDate.GagePoint += distancePoint * 1.2f;

		// ゲージを制限 //
		// Limit gage point //
		if(GameDate.GagePoint > 512) GameDate.GagePoint = 512;

		// ポイントが0以下の時は譜面を削除しない
		if(distancePoint > 0){

			// 譜面の削除
			Destroy (gameObject);

			// エフェクトオブジェクトを生成
			GameObject touchObject = Instantiate(touchRingPrefab);

			// エフェクトの位置の移動とサイズをリセット
			// ※BrokenEffectの大きさを微調整しなければいけないので、ここは重要！
			touchObject.transform.position   = new Vector3(transform.position.x ,
														   transform.position.y + -12f,
														   transform.position.z + -19f);
			
			// BrokenEffectの大きさはtransform.positionとの兼ね合いもあって、
			// 破綻するかもしれないので、要検討!
			touchObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

			// アニメーションを開始
			touchObject.GetComponent<Animator>().Play( 0 );

			// PointTextプレハブ
//			GameObject pointObject = Instantiate(pointText);

			// 初期化
//			pointObject.transform.position = transform.position + new Vector3(0.6f, -5f, -10f);
//			pointText.transform.localScale = new Vector3(7f, 7f, 7f);

			// 再生
//			pointObject.GetComponentInChildren<Animator>().Play( 0 );

			// 評価用テキスト表示
			showText(distancePoint);

			// ポイント表示(スコアを更新)
			pointHandler.setPoint(GameDate.score);

			// ゲージ表示(ゲージを更新)
			gageHandler.setGage(GameDate.GagePoint);
		}
	}

	// 評価用テキスト作成 
	private void showText(float point){

		// PointTextプレハブ（エフェクトオブジェクトを生成
		GameObject pointObj =Instantiate(pointText);
//		GameObject pointObject = Instantiate(pointText);

		// 初期化
//		pointObj.transform.position   = transform.position + new Vector3(0, 1f, 0);
		pointObj.transform.position   = transform.position + new Vector3(0.6f, -5f, -10f);
//		pointObj.transform.localScale = Vector3.one;
		pointText.transform.localScale = new Vector3(7f, 7f, 7f);


		// ポイントに応じて画像を切替
		// Badは『point > 0』で0にしておかないとタッチバーより上でmissが表示されてしまう。
		if(      point > 3.2f )
			pointObj.GetComponentInChildren<SpriteRenderer>().sprite = textSprite[(int)PointTextKey.Perfect];
		else if( point > 2.7f )
			pointObj.GetComponentInChildren<SpriteRenderer>().sprite = textSprite[(int)PointTextKey.Great];
//		else if( point > 2.0f )
//			pointObj.GetComponentInChildren<SpriteRenderer>().sprite = textSprite[(int)PointTextKey.Good];
		else if( point > 2.2f )
			pointObj.GetComponentInChildren<SpriteRenderer>().sprite = textSprite[(int)PointTextKey.Good];
		else if( point > 0    )
			pointObj.GetComponentInChildren<SpriteRenderer>().sprite = textSprite[(int)PointTextKey.Bad];
		else
			pointObj.GetComponentInChildren<SpriteRenderer>().sprite = textSprite[(int)PointTextKey.Miss];	// point＝0ならmissと表示させる

		// アニメーションを開始 //
		pointObj.GetComponentInChildren<Animator>().Play( 0 );

	}


	// 自動で削除
	public void AutoDestroy(){
		
		// 削除
		Destroy(gameObject);

		// 評価用テキスト表示（自動で削除時はshowTextに0を渡す/missを表示させる）
		showText(0);

		// ゲージを減算(missしたときなのでここに記載)
		GameDate.GagePoint -= 2;

		// ゲージを制限
		if(GameDate.GagePoint < 0) GameDate.GagePoint = 0;

		// ゲージを表示
		gageHandler.setGage(GameDate.GagePoint);
	}
}
