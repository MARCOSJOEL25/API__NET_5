using API__NET_5.Models;
using API__NET_5.Models.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API__NET_5
{
    public class ConfigMapper
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<DtoPlayer, player>();
                config.CreateMap<player, DtoPlayer>();
            });

            return mappingConfig;
        }
    }
}
