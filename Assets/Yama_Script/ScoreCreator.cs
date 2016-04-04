using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;	// 型を指定出来るリスト・配列

public class ScoreCreator : MonoBehaviour {

	//Scoreのプレハブ
	public GameObject scorePrefab;

	//タイマー
//	private float timer;

	// Jsonテキストデータ
	public TextAsset jsonDate;

	// 音楽データを格納する構造体の配列
	public List<MusicDate> scoreDate;

	// スコアのXの位置
	private static float[] ScorePositionXList = new float[]{
		-398f, -132.5f, 132.5f, 398f
	};


	// Use this for initialization
	void Start () {

		// scoreDataを初期化
		scoreDate = new List<MusicDate> ();

		// ランダム生成のシードをセット
		Random.seed = 100;

		// タイマーを初期化
//		timer = TimeManager.time + 1f;

		// テキストデータを配列に変換
		// DeserializeはJSONデータを読み込むメソッド
		// IDictionaryはキー付きの配列
		// 型変換も忘れずに（IDictionaryはオブジェクト型で返ってくる）
		IDictionary tmpDate = (IDictionary)Json.Deserialize(jsonDate.text);

		// 値「”score”」に配列が格納されている
		List<object> arrayDate = (List<object>)tmpDate["score"];

		// arrayDataを解析
		foreach(IDictionary val in arrayDate){
			// eventがnote_onの時のみ格納
			if((string)val["event"] == "note_on"){
				
				scoreDate.Add(
					new MusicDate( 
						(long)val["tick"], 
						(int)(long)val["value"]
					)
				);
			}

			// eventがset_tempoの時はテンポ情報（↑『else ifでまとめられそう）
			// 整数系はlong型になってるので一旦変換して再度int型に変換する
			if( (string)val["event"] == "set_tempo" ){
				TimeManager.tempo = (int)(long)val["value"];
			}
		}
	}


	// Update is called once per frame
	void Update () {

//		一定時間(1秒)ごとに譜面をランダム生成→Jsonデータの通りに生成するので必要なくなる
//		if(timer < TimeManager.time){

		// scoreDataをチェック
		foreach( MusicDate tmp in scoreDate ){
			
			// 指定したTickを超えたものから生成
			// 『!tmp.isCreated』がないと、既に生成したものに対して再度生成してしまう
			if (!tmp.isCreated && TimeManager.tick > tmp.tick) {
//			if (!tmp.isCreated) {


				// 譜面を生成
				GameObject scoreObject = Instantiate (scorePrefab);

				// 譜面のヒエラルキーを移動
//				scoreObject.transform.parent = transform;	//この書き方は非推奨
				scoreObject.transform.SetParent (transform);

				// 譜面のXの位置を決定
				int rand = Random.Range (0, ScoreCreator.ScorePositionXList.Length);

				// SAKで追加したボタンアニメーションの修正
				scoreObject.tag = (rand + 1).ToString();

				float x = ScoreCreator.ScorePositionXList[rand];

				// 譜面のYの位置を決定
				float y = tmp.tick * 0.05f + 3000;

				// 譜面の位置を移動※Y軸はボードの移動によって決定
				scoreObject.transform.localPosition = new Vector3 (x, y, 0);

//					x,
//				TimeManager.time * 300 + 2800, //暫定で配置していたyの位置
//					0
//				);

				// 譜面のスケールをリセット
				scoreObject.transform.localScale = Vector3.one;

				//出現したものの表示順を最奥に設定
				scoreObject.transform.SetAsFirstSibling ();

//				タイマー更新(次の時間をセット)/isCreatedで管理するので必要なくなる
//				timer = TimeManager.time + 1f;

				// 生成フラグをセット
				tmp.isCreated = true;
			}
		}
	}
}
