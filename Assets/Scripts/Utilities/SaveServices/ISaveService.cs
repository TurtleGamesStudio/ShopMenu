namespace TurtleGamesStudio.Utilities.SaveServices
{
    public interface ISaveService
    {
        public void Save<T>(string key, T data);
        public T Load<T>(string key);
        public bool HasContainer(string key);
    }
}
