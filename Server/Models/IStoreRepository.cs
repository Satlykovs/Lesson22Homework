namespace Server;

public interface IStoreRepository
{
    List<Game> GetAllGames();
    void AddGame(Game game);
    void DeleteGame(int id);

}