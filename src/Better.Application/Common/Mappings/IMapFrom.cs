using AutoMapper;

namespace Better.Application.Common.Mappings;

// Marker interface
public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}