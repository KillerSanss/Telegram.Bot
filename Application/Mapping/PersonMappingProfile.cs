using Application.Dtos.Person;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.ValueObjects;

namespace Application.Mapping;

/// <summary>
/// Маппинг для Person
/// </summary>
public class PersonMappingProfile : Profile
{
    public PersonMappingProfile()
    {
        CreateMap<Person, PersonGetByIdResponse>()
            .ForAllMembers(opt => opt.MapFrom(s => s.FullName));
        
        CreateMap<Person, PersonGetAllResponse>()
            .ForAllMembers(opt => opt.MapFrom(s => s.FullName));
            
        CreateMap<Person, PersonCreateResponse>()
            .ForAllMembers(opt => opt.MapFrom(s => s.FullName));
        
        CreateMap<Person, PersonUpdateResponse>()
            .ForAllMembers(opt => opt.MapFrom(s => s.FullName));
        
        CreateMap<PersonCreateRequest, Person>()
            .ConstructUsing(dto => new Person(
                Guid.NewGuid(),
                new FullName(dto.FirstName, dto.LastName, dto.MiddleName),
                dto.Gender,
                dto.BirthDate,
                dto.PhoneNumber,
                dto.Telegram));
        
        CreateMap<PersonUpdateRequest, Person>()
            .ConstructUsing(dto => new Person(
                dto.Id,
                new FullName(dto.FirstName, dto.LastName, dto.MiddleName),
                dto.Gender,
                dto.BirthDate,
                dto.PhoneNumber,
                dto.Telegram));
    }
}