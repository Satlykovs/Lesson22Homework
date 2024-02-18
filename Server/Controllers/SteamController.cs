namespace Server;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/")]
public class SteamController : ControllerBase
{
    private readonly IStoreRepository _storeRepository;

    public SteamController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    [HttpGet("store/show")]
    public IActionResult Show()
    {
        return Ok(_storeRepository.GetAllGames());
    }

    [HttpPost("store/add")]
    public IActionResult Add([FromBody] Game game)
    {
        _storeRepository.AddGame(game);
        return Ok(_storeRepository.GetAllGames());   
    }

    [HttpDelete("store/remove")]
    public IActionResult Delete(string name)
    {
        _storeRepository.DeleteGame(name);
        return Ok(_storeRepository.GetAllGames());
    }


}