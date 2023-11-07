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

namespace ASP.groep.L.UnitTesting.Mocks.WorkSchedules
{
    public static class MockWorkScheduleMediator
    {
        public static Mock<IMediator> GetMediator()
        {
            Mock<IWorkSchedulesRepository> _employeeTaskRepo = MockWorkSchedulesRepo.GetWorkSchedulesRepo();
            Mock<IUnitofWork> _uow = new();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            });
            IMapper _mapper = mapperConfig.CreateMapper();


            _uow.Setup(_uow => _uow.WorkSchedulesRepository).Returns(_employeeTaskRepo.Object);
            var _mediator = new Mock<IMediator>();

            //GetAll
            _mediator.Setup(x => x.Send(It.IsAny<GetAllWorkSchedulesQuery>(), CancellationToken.None)).Returns((GetAllWorkSchedulesQuery query, CancellationToken token) =>
            {
                var handler = new GetAllWorkSchedulesQueryHandler(_uow.Object, _mapper);
                Task<IEnumerable<WorkSchedule>> result = handler.Handle(query, token);
                return result;
            }
            );

            //GetbyId
            _mediator.Setup(x => x.Send(It.IsAny<GetWorkScheduleByIdQuery>(), CancellationToken.None)).Returns((GetWorkScheduleByIdQuery query, CancellationToken token) =>
            {
                var handler = new GetWorkScheduleByIdQueryHandler(_uow.Object, _mapper);
                Task<WorkSchedule> result = handler.Handle(query, token);
                return result;
            }
            );


            //Create
            _mediator.Setup(x => x.Send(It.IsAny<CreateWorkScheduleCommand>(), CancellationToken.None)).Returns((CreateWorkScheduleCommand command, CancellationToken token) =>
            {
                var handler = new CreateWorkScheduleCommand.CreateWorkScheduleCommandHandler(_uow.Object, _mapper);
                Task<WorkSchedule> result = handler.Handle(command, token);
                return result;
            }
            );

            //Update
            _mediator.Setup(x => x.Send(It.IsAny<UpdateWorkScheduleCommand>(), CancellationToken.None)).Returns((UpdateWorkScheduleCommand command, CancellationToken token) =>
            {
                var handler = new UpdateWorkScheduleCommand.UpdateWorkScheduleCommandHandler(_uow.Object, _mapper);
                Task<WorkSchedule> result = handler.Handle(command, token);
                return result;
            }
            );

            //Delete
            _mediator.Setup(x => x.Send(It.IsAny<DeleteWorkScheduleCommand>(), CancellationToken.None)).Returns((DeleteWorkScheduleCommand command, CancellationToken token) =>
            {
                var handler = new DeleteWorkScheduleCommand.DeleteWorkScheduleCommandHandler(_uow.Object, _mapper);
                Task<int> result = handler.Handle(command, token);
                return result;
            }
            );
            return _mediator;
        }
    }
}
