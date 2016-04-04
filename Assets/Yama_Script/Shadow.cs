using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	public bool    shadowEnabled    = true;
	public bool    updateEnabled    = true;
	public Vector3 offsetPosition   = new Vector3 (-0.2f, 0.0f, 0.1f);
	public string  sortingLayerName = "UI";
	public int     sortingOrder     = 0;
	public Color   ShadowColor      = new Color (0.0f, 0.0f, 0.0f, 0.5f);

	SpriteRenderer spriteSrc;
	SpriteRenderer spriteCopy;

	// Use this for initialization
	void Start () {

		spriteSrc = GetComponent<SpriteRenderer> ();
	
		GameObject goEmpty = new GameObject ("Shadow");
		spriteCopy         = goEmpty.AddComponent<SpriteRenderer> ();
		spriteCopy         = goEmpty.GetComponent<SpriteRenderer> ();

		goEmpty.transform.parent     = transform;
		goEmpty.transform.localScale = Vector3.one * 1.08f;

//		spriteCopy.tag              = "Shadow";
		spriteCopy.sortingLayerName = sortingLayerName;
		spriteCopy.sortingOrder     = sortingOrder;
		spriteCopy.color            = ShadowColor;

		UpdateShadow ();
	}


	// Update is called once per frame
	void Update () {
		if(updateEnabled){
			UpdateShadow ();
		}
	}

	void UpdateShadow(){
		spriteCopy.transform.position = spriteSrc.transform.position;
		spriteCopy.transform.Translate (-0.2f,0.0f,0.1f,Space.Self);
		spriteCopy.sprite = spriteSrc.sprite;
	}

}
