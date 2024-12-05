namespace Systems.Persistence {
    public interface ISerializer {
        string Serialize<T>(T obj);
        T Deserializer<T>(string json);
     }
}
