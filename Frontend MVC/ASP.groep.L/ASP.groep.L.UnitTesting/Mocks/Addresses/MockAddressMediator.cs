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

namespace ASP.groep.L.UnitTesting.Mocks.Addresses
{
    public static class MockAddressMediator
    {
        public static Mock<IMediator> GetMediator()
        {
            Mock<IAddressesRepository> _addressRepo = MockAddressRepository.GetAddressRepo();
            Mock<IUnitofWork> _uow = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            });

            IMapper _mapper = mapperConfig.CreateMapper();
            _uow.Setup(_uow => _uow.AddressesRepository).Returns(_addressRepo.Object);
            var _mediator = new Mock<IMediator>();

            //GetAll
            _mediator.Setup(x => x.Send(It.IsAny<GetAllAddressesQuery>(), CancellationToken.None))
                .Returns((GetAllAddressesQuery query, CancellationToken token) =>
                {
                    var handler = new GetAllAddressesQueryHandler(_uow.Object, _mapper);
                    Task<IEnumerable<Address>> result = handler.Handle(query, token);
                    return result;
                });

            //GetById
            _mediator.Setup(x => x.Send(It.IsAny<GetAddressByIdQuery>(), CancellationToken.None))
                .Returns((GetAddressByIdQuery query, CancellationToken token) =>
                {
                    var handler = new GetAddressByIdQueryHandler(_uow.Object, _mapper);
                    Task<Address> result = handler.Handle(query, token);
                    return result;
                });

            //Create
            _mediator.Setup(x => x.Send(It.IsAny<CreateAddressCommand>(), CancellationToken.None))
                .Returns((CreateAddressCommand command, CancellationToken token) =>
                {
                    var handler = new CreateAddressCommand.CreateAddressCommandHandler(_uow.Object, _mapper);
                    Task<Address> result = handler.Handle(command, token);
                    return result;
                });

            //Update
            _mediator.Setup(x => x.Send(It.IsAny<UpdateAddressCommand>(), CancellationToken.None))
                .Returns((UpdateAddressCommand command, CancellationToken token) =>
                {
                    var handler = new UpdateAddressCommand.UpdateAddressCommandHandler(_uow.Object, _mapper);
                    Task<Address> result = handler.Handle(command, token);
                    return result;
                });

            //Delete
            _mediator.Setup(x => x.Send(It.IsAny<DeleteAddressCommand>(), CancellationToken.None))
                .Returns((DeleteAddressCommand command, CancellationToken token) =>
                {
                    var handler = new DeleteAddressCommand.DeleteAddressCommandHandler(_uow.Object, _mapper);
                    Task<int> result = handler.Handle(command, token);
                    return result;
                });

            return _mediator;
        }
    }
}
