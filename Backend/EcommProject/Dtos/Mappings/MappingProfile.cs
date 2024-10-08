﻿using AutoMapper;
using EcommProject.Dtos;
using EcommProject.Models;

namespace MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PedidoDto, Pedido>().ReverseMap();
            CreateMap<Pedido, PedidoResponseDto>();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}


