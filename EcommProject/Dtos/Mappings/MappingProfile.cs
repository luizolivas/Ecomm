using AutoMapper;
using EcommProject.Dtos;
using EcommProject.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PedidoDto, Pedido>();
        CreateMap<Pedido, PedidoResponseDto>();
    }
}
