﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Router : MonoBehaviour {

	public void ShowPausePanel(){
		GAMECtrl.instance.ShowPauseMenu ();
	}

	public void HidePausePanel(){
		GAMECtrl.instance.HidePauseMenu ();
	}
}
