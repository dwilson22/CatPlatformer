using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
[Serializable]
public class UI {

	[Header("Texts")]
	public Text txtCoinCount;
	public Text txtScore;
	public Text txtTimer;
	[Header("Images")]
	public Image key0;
	public Image key1;
	public Image key2;
	public Image heart0;
	public Image heart1;
	public Image heart2;
	[Header("Sprites")]
	public Sprite keyFull0;
	public Sprite keyFull1;
	public Sprite keyFull2;
	public Sprite heartEmpty;
	public Sprite heartFull;

	[Header("GameObjects")]
	public GameObject panelGameOver;
}
