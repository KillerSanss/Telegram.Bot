using Application.Dtos.Person;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Api;

/// <summary>
/// Констроллер персоны
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    /// <summary>
    /// Получение всех персон из базы данных
    /// </summary>
    /// <param name="personService">Сервис персоны.</param>
    /// <returns>Все персоны в базе данных.</returns>
    [HttpGet("get_all")]
    public IActionResult GetAll([FromServices] PersonService personService)
    {
        var persons = personService.GetAll();
        return Ok(persons);
    }
    
    /// <summary>
    /// Получение персоны по идентификатору
    /// </summary>
    /// <param name="personService">Сервис персоны.</param>
    /// <param name="id">Идентификатор персоны.</param>
    /// <returns>Выбранная персона.</returns>
    [HttpGet("get_by_id")]
    public IActionResult GetById([FromServices] PersonService personService, Guid id)
    {
        var person = personService.GetById(id);
        return Ok(person);
    }
    
    /// <summary>
    /// Добавление персоны в базу
    /// </summary>
    /// <param name="personService">Сервис персоны.</param>
    /// <param name="request">Данные персоны на добавление.</param>
    /// <returns>Добавленная персона.</returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromServices] PersonService personService, [FromBody] PersonCreateRequest request)
    {
        var addedPerson = await personService.Create(request);
        return Ok(addedPerson);
    }
    
    /// <summary>
    /// Обновление персоны в базе
    /// </summary>
    /// <param name="personService">Сервис персоны.</param>
    /// <param name="request">Данные персоны на обновление.</param>
    /// <returns>Обновленная персона.</returns>
    [HttpPut("update")]
    public IActionResult Update([FromServices] PersonService personService, [FromBody] PersonUpdateRequest request)
    {
        var updatedPerson = personService.Update(request);
        return Ok(updatedPerson);
    }

    /// <summary>
    /// Удаление персоны из базы
    /// </summary>
    /// <param name="personService">Сервис персоны.</param>
    /// <param name="id">Идентификатор персоны.</param>
    [HttpDelete("delete")]
    public IActionResult Delete([FromServices] PersonService personService, Guid id)
    {
        personService.Delete(id);
        return NoContent();
    }
}