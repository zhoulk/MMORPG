//===================================================
//作    者：周连康 
//创建时间：2018-12-04 17:45:23
//备    注：
//===================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Game.Instance.Init();

        CtrlManager.Instance.OpenCtrl(CtrlNames.UILoading);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
