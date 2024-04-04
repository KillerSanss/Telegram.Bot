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
        CreateMap<Person, PersonDtoResponse>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(s => s.FullName.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(s => s.FullName.LastName))
            .ForMember(dest => dest.MiddleName,
                opt => opt.MapFrom(s => s.FullName.MiddleName));
        
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