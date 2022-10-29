namespace VideoLlamada.TokenGenerator.AgoraIO
{
    public interface IPackable
    {
        ByteBuf marshal(ByteBuf outBuf);
    }
}
