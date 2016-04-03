using UnityEngine;
using System.Collections;

// 単独で使うのでMonoBehaviorはいらない
public class MusicDate  {

	// 音楽譜面データを格納しておく構造体 //

	// Tick
//	public long  tick;
	public float tick;

	// value(note_onなら音程でset_tempoならBPMを司る)
	public int value;

	// すでに譜面を生成したかどうかを判別するフラグ
	public bool isCreated;

	// コンストラクタ
//	public MusicDate(long tick, int value){
	public MusicDate(float tick, int value){

		// 値を格納
		this.tick  =  tick;
		this.value =  value;

		// 初期化
		this.isCreated = false;

	}
}
