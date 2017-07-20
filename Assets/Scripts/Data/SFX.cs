using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// groups particle effects
/// </summary>
[Serializable]
public class SFX  {
	[Header("GameObjects")]
	public GameObject sfx_coin_pickup;
	public GameObject sfx_bullet_pickup;
	public GameObject sfx_playerlands;
	public GameObject sfx_box_fragment_1;
	public GameObject sfx_box_fragment_2;
	public GameObject sfx_splash;
	public GameObject sfx_enemy_explosion;

	[Header("Transforms")]
	public Transform key0;
	public Transform key1;
	public Transform key2;
		

}
