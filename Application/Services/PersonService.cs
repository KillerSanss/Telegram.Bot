using Application.Dtos.Person;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Application.Services;

/// <summary>
/// Сервис для Person
/// </summary>
public class PersonService
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
    /// <returns>Персона.</returns>
    public PersonGetByIdResponse GetById(Guid id)
    {
        var person = GetByIdOrThrow(id);
        return _mapper.Map<PersonGetByIdResponse>(person);
    }

    /// <summary>
    /// Получение всех Person
    /// </summary>
    /// <returns>Список всех Person.</returns>
    public List<PersonGetAllResponse> GetAll()
    {
        var persons = _personRepository.GetAll();
        return _mapper.Map<List<PersonGetAllResponse>>(persons);
    }

    /// <summary>
    /// Создание Person
    /// </summary>
    /// <param name="personCreateRequest">Person на создание.</param>
    /// <returns>Созданный Person</returns>
    public PersonCreateResponse CreateAsync(PersonCreateRequest personCreateRequest)
    {
        Guard.Against.Null(personCreateRequest);

        var person = _mapper.Map<Person>(personCreateRequest);
         _personRepository.Create(person);

        return _mapper.Map<PersonCreateResponse>(person);
    }

    /// <summary>
    /// Обновление Person
    /// </summary>
    /// <param name="personUpdateRequest">Person на обновление.</param>
    /// <returns>Обновленный Person.</returns>
    public PersonUpdateResponse Update(PersonUpdateRequest personUpdateRequest)
    {
        Guard.Against.Null(personUpdateRequest);

        var person = GetByIdOrThrow(personUpdateRequest.Id);
        
        person.FullName = new FullName(
            personUpdateRequest.FirstName,
            personUpdateRequest.LastName,
            personUpdateRequest.MiddleName);
        person.Gender = personUpdateRequest.Gender;
        person.BirthDate = personUpdateRequest.BirthDate;
        person.PhoneNumber = personUpdateRequest.PhoneNumber;
        person.Telegram = personUpdateRequest.Telegram;
        
        _personRepository.Update(person);
        
        return _mapper.Map<PersonUpdateResponse>(person);
    }

    /// <summary>
    /// Удаление Person
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    public void Delete(Guid id)
    {
        var person = GetByIdOrThrow(id);
        _personRepository.Delete(person);
    }

    /// <summary>
    /// Метод проверки на наличие объекта
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <returns>Person.</returns>
    private Person GetByIdOrThrow(Guid id)
    {
        var person = _personRepository.GetById(id);
        if (person == null)
        {
            throw new EntityNotFoundException<Person>(nameof(Person.Id), id.ToString());
        }

        return person;
    }
}