using Google.Protobuf;
using System;

public interface IProtoParser {

    ByteBuffer encode(IMessage obj);
    IMessage decode(byte[] bytes);

    void RegisteProtoParser(int id, Type type, MessageParser parser);
}
