//===================================================
//作    者：周连康 
//创建时间：2018-12-04 15:37:44
//备    注：
//===================================================

using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginViewCtrl : BaseCtrl
{
    public override void Start(params object[] args)
    {
        this.view = ViewManager.Instance.CreateView(this, PanelNames.UILogin, args);
    }

    public void ShowRegView()
    {
        CtrlManager.Instance.OpenCtrl(CtrlNames.UIRegiste);
        CtrlManager.Instance.CloseCtrl(CtrlNames.UILogin);
    }

    public void Login(string name, string password)
    {
        Login login = new Login();
        login.Account = name;
        login.Passward = password;
        LoginLogic.Instance.Login(login);
    }
}
