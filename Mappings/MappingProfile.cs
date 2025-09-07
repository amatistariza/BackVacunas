using AutoMapper;
using API.Domain.Models;
using API.DTO;

namespace API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from Create DTOs to Entities (for requests)
            CreateMap<PacienteCreateDTO, Paciente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID for creation

            CreateMap<CondicionUsuariaCreateDTO, CondicionUsuaria>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore());

            CreateMap<AntecedentesMedicosCreateDTO, AntecedentesMedicos>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore());

            CreateMap<AntecedenteCreateDTO, Antecedente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore());

            // Note: For now we'll handle entity-to-DTO mapping manually in services
            // because we need to find the existing DTOs in the DTO folder
        }
    }
}
