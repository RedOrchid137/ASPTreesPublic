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

namespace ASP.groep.L.UnitTesting.Mocks.Sites
{
    public static class MockSiteMediator
    {
        public static Mock<IMediator> GetMediator()
        {
            Mock<ISitesRepository> _siteRepo = MockSiteRepository.GetSiteRepo();
            Mock<IUnitofWork> _uow = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            });

            IMapper _mapper = mapperConfig.CreateMapper();
            _uow.Setup(_uow => _uow.SitesRepository).Returns(_siteRepo.Object);
            var _mediator = new Mock<IMediator>();

            //GetAll
            _mediator.Setup(x => x.Send(It.IsAny<GetAllSitesQuery>(), CancellationToken.None))
                .Returns((GetAllSitesQuery query, CancellationToken token) =>
                {
                    var handler = new GetAllSitesQueryHandler(_uow.Object, _mapper);
                    Task<IEnumerable<Site>> result = handler.Handle(query, token);
                    return result;
                });

            //GetById
            _mediator.Setup(x => x.Send(It.IsAny<GetSiteByIdQuery>(), CancellationToken.None))
                .Returns((GetSiteByIdQuery query, CancellationToken token) =>
                {
                    var handler = new GetSiteByIdQueryHandler(_uow.Object, _mapper);
                    Task<Site> result = handler.Handle(query, token);
                    return result;
                });

            //Create
            _mediator.Setup(x => x.Send(It.IsAny<CreateSiteCommand>(), CancellationToken.None))
                .Returns((CreateSiteCommand command, CancellationToken token) =>
                {
                    var handler = new CreateSiteCommand.CreateSiteCommandHandler(_uow.Object, _mapper);
                    Task<Site> result = handler.Handle(command, token);
                    return result;
                });

            //Update
            _mediator.Setup(x => x.Send(It.IsAny<UpdateSiteCommand>(), CancellationToken.None))
                .Returns((UpdateSiteCommand command, CancellationToken token) =>
                {
                    var handler = new UpdateSiteCommand.UpdateSiteCommandHandler(_uow.Object, _mapper);
                    Task<Site> result = handler.Handle(command, token);
                    return result;
                });

            //Delete
            _mediator.Setup(x => x.Send(It.IsAny<DeleteSiteCommand>(), CancellationToken.None))
                .Returns((DeleteSiteCommand command, CancellationToken token) =>
                {
                    var handler = new DeleteSiteCommand.DeleteSiteCommandHandler(_uow.Object, _mapper);
                    Task<int> result = handler.Handle(command, token);
                    return result;
                });

            return _mediator;
        }
    }
}
