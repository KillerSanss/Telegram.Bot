using Application.Dtos.Person;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Application.Services;

/// <summary>
/// Сервис для Person
/// </summary>
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение Person
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Персона.</returns>
    public async Task<PersonDtoResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var person = await GetByIdOrThrowAsync(id, cancellationToken);
        return _mapper.Map<PersonDtoResponse>(person);
    }

    /// <summary>
    /// Получение всех Person
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех Person.</returns>
    public async Task<List<PersonDtoResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<PersonDtoResponse>>(persons);
    }

    /// <summary>
    /// Создание Person
    /// </summary>
    /// <param name="personCreateRequest">Person на создание.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный Person</returns>
    public async Task<PersonDtoResponse> CreateAsync(PersonCreateRequest personCreateRequest, CancellationToken cancellationToken)
    {
        Guard.Against.Null(personCreateRequest);

        var person = _mapper.Map<Person>(personCreateRequest);
        await _personRepository.CreateAsync(person, cancellationToken);

        return _mapper.Map<PersonDtoResponse>(person);
    }

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="personUpdateRequest">Person на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленный Person.</returns>
    public async Task<PersonDtoResponse> UpdateAsync(PersonUpdateRequest personUpdateRequest, CancellationToken cancellationToken)
    {
        Guard.Against.Null(personUpdateRequest);

        var person = await GetByIdOrThrowAsync(personUpdateRequest.Id, cancellationToken);
        
        person.FullName = new FullName(
            personUpdateRequest.FirstName,
            personUpdateRequest.LastName,
            personUpdateRequest.MiddleName);
        person.Gender = personUpdateRequest.Gender;
        person.BirthDate = personUpdateRequest.BirthDate;
        person.PhoneNumber = personUpdateRequest.PhoneNumber;
        person.Telegram = personUpdateRequest.Telegram;
        
        await _personRepository.UpdateAsync(person, cancellationToken);
        
        return _mapper.Map<PersonDtoResponse>(person);
    }

    /// <summary>
    /// Удаление Person
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var person = await GetByIdOrThrowAsync(id, cancellationToken);
        await _personRepository.DeleteAsync(person, cancellationToken);
    }

    /// <summary>
    /// Метод проверки на наличие объекта
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Person.</returns>
    private async Task<Person> GetByIdOrThrowAsync(Guid id, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(id, cancellationToken);
        if (person == null)
        {
            throw new EntityNotFoundException<Person>(nameof(Person.Id), id.ToString());
        }

        return person;
    }
}