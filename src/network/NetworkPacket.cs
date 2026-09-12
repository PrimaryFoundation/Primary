// класс нетворк пакет имеет 3 поля - Type(тип enum), Payload(string, передаваемая инфа серверу) и RequestId(Guid который генерируется ОДИН раз на ОДНУ инициализацию)
namespace Primary.Models;
public class NetworkPacket
{
   public enum Type
    {
      Register,
      Login,
      SearchUser,
      SearchPost,
    }
    public string Payload {get; set;}
    public Guid RequestId {get; set;}
    public Type NetworkType {get; set;}

    public NetworkPacket(string payload, Type type)
    {
        Payload = payload;
        NetworkType = type;
        RequestId = Guid.NewGuid();
    }
    

}   