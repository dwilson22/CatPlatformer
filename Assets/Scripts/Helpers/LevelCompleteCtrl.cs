using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class LevelCompleteCtrl : MonoBehaviour {
	public Button btnNext;
	public Sprite goldStar;
	public Image star1, star2, star3;
	public Text txtScore;
	public int levelNumber, scoreFor1Star,
	           scoreFor2Star, scoreFor3Star, scoreForNextLevel;
	[HideInInspector]
	public int score;
	public float animStartDelay, animDelay;

	bool show2stars, show3stars;
	// Use this for initialization
	void Start () {
		score = GAMECtrl.instance.getScore ();
		txtScore.text = "SCORE : " + score;
		if (score >= scoreFor3Star) {
			show3stars = true;
			GAMECtrl.instance.SetStarsAwarded (levelNumber, 3);
			Invoke ("ShowGoldenStars", animStartDelay);
		}
		if (score >= scoreFor2Star && score <scoreFor3Star) {
			show2stars = true;
			GAMECtrl.instance.SetStarsAwarded (levelNumber, 2);
			Invoke ("ShowGoldenStars", animStartDelay);
		}


		if(score<=scoreFor1Star && score !=0) {
			GAMECtrl.instance.SetStarsAwarded (levelNumber, 1);
			Invoke ("ShowGoldenStars", animStartDelay);
		}
		if (score >= scoreForNextLevel) {
			int newLevelNumber = levelNumber+1;
			btnNext.interactable = true;
			SFXCtrl.instance.ShowBulletSparkle (btnNext.transform.position);
			if (levelNumber >= 3) {
				 newLevelNumber = levelNumber;
			}
			Debug.Log ("LEVEL NUM" + newLevelNumber);
			GAMECtrl.instance.UnlockNextLevel (newLevelNumber);
		}
	}

	void ShowGoldenStars(){
		StartCoroutine ("HandleFirstStarAnim", star1);
	}
	IEnumerator HandleFirstStarAnim(Image star){
		DoStarAnim (star);
		yield return new WaitForSeconds (animDelay);
		if (show2stars || show3stars) {
			StartCoroutine ("HandleSecondStarAnim", star2);
		}
			
	}

	IEnumerator HandleSecondStarAnim(Image star){
		DoStarAnim (star);
		yield return new WaitForSeconds (animDelay);

		if (show3stars) {
			StartCoroutine ("HandleThirdStarAnim", star3);
		}
	}
	IEnumerator HandleThirdStarAnim(Image star){
		DoStarAnim (star);

		yield return new WaitForSeconds (animDelay);


	}

	void DoStarAnim(Image star){
		RectTransform t = star.rectTransform;
		t.sizeDelta = new Vector2 (150f, 150f);
		star.sprite = goldStar;
		t.DOSizeDelta (new Vector2 (100f, 100f), .5f, false);
		SFXCtrl.instance.ShowBulletSparkle (star.gameObject.transform.position);
		AudioCtrl.instance.KeyFound (star.gameObject.transform.position);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
