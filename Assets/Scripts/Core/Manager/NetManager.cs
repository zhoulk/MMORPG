using Google.Protobuf;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : Singleton<NetManager> {

    /// <summary>
    /// ������
    /// </summary>
    IProtoParser m_ProtoParser;
    public IProtoParser ProtoParser
    {
        get
        {
            return m_ProtoParser;
        }
        set
        {
            m_ProtoParser = value;
        }
    }

    SocketClient m_SocketClient;
    public SocketClient SocketClient
    {
        get
        {
            if (m_SocketClient == null)
            {
                m_SocketClient = new SocketClient();
                m_SocketClient.OnConnectHandler += OnConnectHandler;
                m_SocketClient.OnDisconnectHandler += OnDisconnectHandler;
                m_SocketClient.OnReceiveHandler += OnReceiveHandler;
            }
            return m_SocketClient;
        }
    }

    Queue<byte[]> m_BytesQueue = new Queue<byte[]>();
    Queue<IMessage> m_IMessageQueue = new Queue<IMessage>();

    public event OnConnect OnClientConnectHandler;
    public event OnDisconnect OnClientDisconnectHandler;
    public event OnReceive OnClientReceiveHandler;
    public event OnProtoReceive OnClientReceiveProtoHandler;

    /// <summary>
    /// ��ʼ��
    /// </summary>
	public void Init()
    {
        SocketClient.OnRegister();
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public void Connect(string ip, int port)
    {
        SocketClient.ConnectServer(ip, port);
    }

    /// <summary>
    /// ������Ϣ
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMessage(ByteBuffer buffer)
    {
        SocketClient.SendMessage(buffer);
    }

    /// <summary>
    /// ����protoBuffer��Ϣ
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(IMessage message)
    {
        if (m_ProtoParser == null)
        {
            Debug.LogError("û������ Proto ������");
            return;
        }
        ByteBuffer buffer = m_ProtoParser.encode(message);
        SocketClient.SendMessage(buffer);
    }

    /// <summary>
    /// �ر�����
    /// </summary>
    public void Close()
    {
        SocketClient.OnRemove();
    }

    void OnConnectHandler(SocketClient client)
    {
        if (OnClientConnectHandler != null)
        {
            OnClientConnectHandler(client);
        }
    }

    void OnDisconnectHandler(SocketClient client, string msg)
    {
        if (OnClientDisconnectHandler != null)
        {
            OnClientDisconnectHandler(client, msg);
        }
    }

    void OnReceiveHandler(byte[] bytes)
    {
        m_BytesQueue.Enqueue(bytes);
        if (m_ProtoParser != null)
        {
            m_IMessageQueue.Enqueue(m_ProtoParser.decode(bytes));
        }
    }

    public void OnUpdate()
    {
        if (OnClientReceiveHandler != null)
        {
            while (m_BytesQueue.Count > 0)
            {
                byte[] bytes = m_BytesQueue.Dequeue();
                OnClientReceiveHandler(bytes);
            }
        }
        if (OnClientReceiveProtoHandler != null)
        {
            if (m_ProtoParser == null)
            {
                Debug.LogError("û������ Proto ������");
            }
            else
            {
                while (m_IMessageQueue.Count > 0)
                {
                    IMessage message = m_IMessageQueue.Dequeue();
                    OnClientReceiveProtoHandler(message);
                }
            }
        }
    }
}
