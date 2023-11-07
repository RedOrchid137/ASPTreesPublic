﻿using ASP.groep.L.Application.CQRS.DTOS;
using ASP.groep.L.Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.groep.L.Domain;

namespace ASP.groep.L.Application.CQRS.Queries
{
    public class GetAllZonesQuery : IRequest<IEnumerable<Zone>>
    {
        public int pageNr { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
    public class GetAllZonesQueryHandler : IRequestHandler<GetAllZonesQuery, IEnumerable<Zone>>
    {
        private readonly IUnitofWork _uow;
        private readonly IMapper _mapper;

        public GetAllZonesQueryHandler(IUnitofWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Zone>> Handle(GetAllZonesQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.ZonesRepository.GetAll(request.pageNr, request.pageSize);
            return list;
        }
    }
}
