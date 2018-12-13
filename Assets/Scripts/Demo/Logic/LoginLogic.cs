using Google.Protobuf;
using Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginLogic : Singleton<LoginLogic> {

    public void Init()
    {
        NetManager.Instance.OnClientReceiveProtoHandler += OnClientReceiveProtoHandler;
    }

    public void Login(Login login)
    {
        NetManager.Instance.SendMessage(login);
    }

    void OnClientReceiveProtoHandler(IMessage message)
    {
        Debug.Log("OnClientReceiveProtoHandler " + message.GetType());
        if (message.GetType().Equals(typeof(LoginSuccessfull)))
        {
            LoginSuccessfull loginSuccessfull = message as LoginSuccessfull;
            Debug.Log("OnClientReceiveProtoHandler " + loginSuccessfull.PlayerBaseInfo.PlayerID);
        }
    }
}
