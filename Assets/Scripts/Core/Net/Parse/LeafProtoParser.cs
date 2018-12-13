using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeafProtoParser : IProtoParser
{
    /// <summary>
    /// 记录有效的Proto类型
    /// </summary>
    List<int> m_ProtoIdList = new List<int>();
    List<Type> m_ProtoTypeList = new List<Type>();
    Dictionary<RuntimeTypeHandle, MessageParser> m_Parsers = new Dictionary<RuntimeTypeHandle, MessageParser>();

    /// <summary>
    /// 注册proto解析器
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <param name="parser"></param>
    public void RegisteProtoParser(int id, Type type, MessageParser parser)
    {
        if (m_Parsers.ContainsKey(type.TypeHandle))
        {
            Debug.LogError("类型 " + type + " 已经存在！！！");
        }
        else
        {
            m_ProtoIdList.Add(id);
            m_ProtoTypeList.Add(type);
            m_Parsers.Add(type.TypeHandle, parser);
        }
    }

    public IMessage decode(byte[] bytes)
    {
        ByteBuffer buffer = new ByteBuffer(bytes);
        int mainId = buffer.ReadShort();
        int pbDataLen = bytes.Length - 2;
		byte[] pbData = buffer.ReadBytes(pbDataLen);

        if (!m_ProtoIdList.Contains(mainId))
        {
            Debug.LogError("未知协议号");
            return null;
        }
        Type protoType = GetTypeByProtoId(mainId);
        try
        {
            MessageParser messageParser = GetParserByType(protoType);
            IMessage toc = messageParser.ParseFrom(pbData);
            return toc;
        }
        catch
        {
            Debug.LogError("decode Error:" + protoType.ToString());
        }
        return null;
    }

    public ByteBuffer encode(IMessage obj)
    {
        ByteBuffer buff = new ByteBuffer();
        int protoId = GetProtoIdByType(obj.GetType());

        byte[] result;
        using (MemoryStream ms = new MemoryStream())
        {
            obj.WriteTo(ms);
            result = ms.ToArray();
        }

        UInt16 lengh = (UInt16)(result.Length + 2);
        Debug.Log("lengh" + lengh + ",protoId" + protoId);
        buff.WriteShort((UInt16)lengh);
        PrintBytes(buff.ToBytes());

        buff.WriteShort((UInt16)protoId);
        PrintBytes(buff.ToBytes());

        buff.WriteBytes(result);

        PrintBytes(buff.ToBytes());

        PrintBytes(result);

        return buff;
    }

    void PrintBytes(byte[] buffer)
    {
        string returnStr = string.Empty;
        for (int i = 0; i < buffer.Length; i++)
        {
            returnStr += buffer[i].ToString("X2");
        }
        Debug.Log(returnStr);
    }

    int GetProtoIdByType(Type t)
    {
        int index = m_ProtoTypeList.IndexOf(t);
        if (index >= 0)
        {
            return m_ProtoIdList[index];
        }
        return -1;
    }

    Type GetTypeByProtoId(int protoId)
    {
        int index = m_ProtoIdList.IndexOf(protoId);
        if (index >= 0)
        {
            return m_ProtoTypeList[index];
        }
        return null;
    }

    MessageParser GetParserByType(Type type)
    {
        MessageParser parser;
        m_Parsers.TryGetValue(type.TypeHandle, out parser);
        return parser;
    }
}
