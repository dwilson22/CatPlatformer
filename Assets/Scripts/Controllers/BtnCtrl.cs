using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// controls wether a level is unlocked and how many stars show
/// </summary>
public class BtnCtrl : MonoBehaviour {
	int levelNumber;
	Button btn;
	Image btnImg;
	Text btnTex;
	Transform star1, star2, star3;
	public Sprite lockedBtn;
	public Sprite unlockedBtn;
	public string sceneName;
	// Use this for initialization
	void Start () {
		levelNumber = int.Parse (transform.gameObject.name);
		btn = transform.gameObject.GetComponent<Button> ();
		btnImg = btn.GetComponent<Image>();
		btnTex = btn.gameObject.transform.GetChild (0).GetComponent<Text> ();
		star1 = btn.gameObject.transform.GetChild (1);
		star2 = btn.gameObject.transform.GetChild (2);
		star3 = btn.gameObject.transform.GetChild (3);

		BtnStatus ();
	}
	
	void BtnStatus(){
		bool unlocked = DataCtrl.instance.isUnlocked (levelNumber);
		int starsAwarded = DataCtrl.instance.getStarsAwarded (levelNumber);
		if (unlocked) {
			if (starsAwarded == 3) {
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (true);
				star3.gameObject.SetActive (true);
			}

			if (starsAwarded == 2) {
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (true);
				star3.gameObject.SetActive (false);
			}

			if (starsAwarded == 1) {
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (false);
				star3.gameObject.SetActive (false);
			}

			if (starsAwarded == 0) {
				star1.gameObject.SetActive (false);
				star2.gameObject.SetActive (false);
				star3.gameObject.SetActive (false);
			}

			btn.onClick.AddListener (LoadScene);
		} else {
			btnImg.overrideSprite = lockedBtn;
			btnTex.text = "";
			star1.gameObject.SetActive (false);
			star2.gameObject.SetActive (false);
			star3.gameObject.SetActive (false);
		}
	}

	void LoadScene(){
		SceneManager.LoadScene (sceneName);
	}
}
