using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnConnect(SocketClient client);

public delegate void OnDisconnect(SocketClient client, string errMsg);

public delegate void OnReceive(byte[] bytes);

public delegate void OnProtoReceive(IMessage message);
