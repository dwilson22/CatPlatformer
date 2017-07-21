using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData  {

	public int coinCount;
	public int score;
	public bool[] keyFound;
	public int lives;
	public bool isFirstBoot;
	public LevelData[] levelData;
}
