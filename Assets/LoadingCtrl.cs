using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCtrl : MonoBehaviour {
	public GameObject panelLoading;

	public static LoadingCtrl instance;
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
	}
	
	public void ShowLoading(){
		panelLoading.SetActive (true);
	}
}
