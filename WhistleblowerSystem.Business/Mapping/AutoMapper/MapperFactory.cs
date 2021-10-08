using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleblowerSystem.Business.Mapping.AutoMapper
{
    public class MapperFactory
    {
        static IMapper _mapper = null!;

        public static IMapper Create()
        {
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            _mapper = mapperConfig.CreateMapper();

            return _mapper;
        }
    }
}