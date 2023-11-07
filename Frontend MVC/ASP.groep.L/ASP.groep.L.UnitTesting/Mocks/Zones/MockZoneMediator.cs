using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.CQRS.Mappings;
using ASP.groep.L.Application.CQRS.Queries;
using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASP.groep.L.UnitTesting.Mocks.Zones
{
    public static class MockZoneMediator
    {
        public static Mock<IMediator> GetMediator()
        {
            Mock<IZonesRepository> _zoneRepo = MockZonesRepo.GetZonesRepo();
            Mock<IUnitofWork> _uow = new();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            });
            IMapper _mapper = mapperConfig.CreateMapper();


            _uow.Setup(_uow => _uow.ZonesRepository).Returns(_zoneRepo.Object);
            var _mediator = new Mock<IMediator>();

            //GetAll
            _mediator.Setup(x => x.Send(It.IsAny<GetAllZonesQuery>(), CancellationToken.None)).Returns((GetAllZonesQuery query, CancellationToken token) =>
            {
                var handler = new GetAllZonesQueryHandler(_uow.Object, _mapper);
                Task<IEnumerable<Zone>> result = handler.Handle(query, token);
                return result;
            }
            );

            //GetbyId
            _mediator.Setup(x => x.Send(It.IsAny<GetZoneByIdQuery>(), CancellationToken.None)).Returns((GetZoneByIdQuery query, CancellationToken token) =>
            {
                var handler = new GetZoneByIdQueryHandler(_uow.Object, _mapper);
                Task<Zone> result = handler.Handle(query, token);
                return result;
            }
            );


            //Create
            _mediator.Setup(x => x.Send(It.IsAny<CreateZoneCommand>(), CancellationToken.None)).Returns((CreateZoneCommand command, CancellationToken token) =>
            {
                var handler = new CreateZoneCommand.CreateZoneCommandHandler(_uow.Object, _mapper);
                Task<Zone> result = handler.Handle(command, token);
                return result;
            }
            );

            //Update
            _mediator.Setup(x => x.Send(It.IsAny<UpdateZoneCommand>(), CancellationToken.None)).Returns((UpdateZoneCommand command, CancellationToken token) =>
            {
                var handler = new UpdateZoneCommand.UpdateZoneCommandHandler(_uow.Object, _mapper);
                Task<Zone> result = handler.Handle(command, token);
                return result;
            }
            );

            //Delete
            _mediator.Setup(x => x.Send(It.IsAny<DeleteZoneCommand>(), CancellationToken.None)).Returns((DeleteZoneCommand command, CancellationToken token) =>
            {
                var handler = new DeleteZoneCommand.DeleteZoneCommandHandler(_uow.Object, _mapper);
                Task<int> result = handler.Handle(command, token);
                return result;
            }
            );
            return _mediator;
        }
    }
}
