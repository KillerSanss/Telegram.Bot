using Application.Dtos.Person;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Ardalis.GuardClauses;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

/// <summary>
/// Сервис для Person
/// </summary>
public class PersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
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
    /// <returns>Созданный Person.</returns>
    public async Task<PersonCreateResponse> Create(PersonCreateRequest personCreateRequest)
    {
        Guard.Against.Null(personCreateRequest);
        var person = _mapper.Map<Person>(personCreateRequest);
        var createdPerson = _personRepository.Create(person);
        await _personRepository.SaveChanges();
        return _mapper.Map<PersonCreateResponse>(createdPerson);
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

        person.Update(
            personUpdateRequest.FullName.FirstName,
            personUpdateRequest.FullName.LastName,
            personUpdateRequest.FullName.MiddleName,
            personUpdateRequest.PhoneNumber,
            personUpdateRequest.Gender,
            personUpdateRequest.BirthDate,
            personUpdateRequest.Telegram);
        
        _personRepository.Update(person);
        _personRepository.SaveChanges();
        
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
        _personRepository.SaveChanges();
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