using Msg;
using UnityEngine;

public class NetModule : Singleton<NetModule>{

    public void Init()
    {
        IProtoParser protoParser = new LeafProtoParser();

        protoParser.RegisteProtoParser(0, typeof(Login), Login.Parser);
        protoParser.RegisteProtoParser(1, typeof(LoginSuccessfull), LoginSuccessfull.Parser);
        //protoParser.RegisteProtoParser(3, typeof(LoginFaild), LoginFaild.Parser);

        NetManager.Instance.ProtoParser = protoParser;

        NetManager.Instance.OnClientConnectHandler += OnClientConnectHandler;
        NetManager.Instance.OnClientDisconnectHandler += OnClientDisconnectHandler;

        NetManager.Instance.Connect(Define.IP, Define.port);
    }

    void OnClientConnectHandler(SocketClient client)
    {
        Debug.Log("服务器连接成功");
    }

    void OnClientDisconnectHandler(SocketClient client, string msg)
    {
        Debug.Log("服务器断开连接  " + msg);
    }
}
