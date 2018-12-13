//===================================================
//作    者：周连康 
//创建时间：2018-12-04 15:37:44
//备    注：
//===================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoginView : BaseView{
    UILoginViewCtrl _iCtrl;

    Button loginBtn;
    Button regBtn;

    public override string PrefabPath()
    {
        return "UIPrefab/Login/UILoginView";
    }

    public override void StartView(params object[] args) {
        _iCtrl = (UILoginViewCtrl)iCtrl;

        loginBtn = this.transform.Find("center/loginBtn").GetComponent<Button>();
        loginBtn.onClick.AddListener(OnLoginBtnClick);

        regBtn = this.transform.Find("center/registerBtn").GetComponent<Button>();
        regBtn.onClick.AddListener(OnRegBtnClick);
    }

    void OnRegBtnClick()
    {
        _iCtrl.ShowRegView();
    }

    void OnLoginBtnClick()
    {
        _iCtrl.Login("123", "456");
    }
}
