using UnityEngine;
using System.Collections;

public class Shadow_AfterImage : MonoBehaviour {

	public SpriteRenderer SpriteSrc;
	public bool afterImageEnabled;

	// Use this for initialization
	void Start () {

		afterImageEnabled = false;

		StartCoroutine ("AfterImageUpdate");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator AfterImageUpdate(){
		while(true){
			while(afterImageEnabled){
				//残像のゲームオブジェクト作成
				SpriteRenderer spriteCopy       = Instantiate(SpriteSrc) as SpriteRenderer;
				spriteCopy.transform.position   = SpriteSrc.transform.position;
				spriteCopy.transform.localScale = SpriteSrc.transform.parent.transform.localScale;

				spriteCopy.color            = new Color (1.0f, 0.0f, 0.0f, 0.5f);
				spriteCopy.sortingLayerName = "UI";
				spriteCopy.sortingOrder     = 1;

				spriteCopy.GetComponent<Shadow> ().enabled = false;
				SpriteRenderer[] spList = spriteCopy.GetComponentsInChildren<SpriteRenderer> ();

				// Shadowスクリプトの項目と一致してなかったら反映されないので注意
				foreach(SpriteRenderer sp in spList){
					if(sp.name == "Shadow"){
						sp.enabled = false;
					}
				}
				// 消すタイミングを微調整すること
				Destroy (spriteCopy.gameObject, 0.3f);
				yield return new WaitForSeconds (0.05f);
			}

			yield return new WaitForSeconds (1.0f);
		}
	}
}
