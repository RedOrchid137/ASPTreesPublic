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

namespace ASP.groep.L.UnitTesting.Mocks.EmployeeTasks
{
    public static class MockEmployeeTaskMediator
    {
        public static Mock<IMediator> GetMediator()
        {
            Mock<IEmployeeTasksRepository> _employeeTaskRepo = MockEmployeeTaskRepo.GetEmployeeTasks();
            Mock<IUnitofWork> _uow = new();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Mappings>();
            });
            IMapper _mapper = mapperConfig.CreateMapper();


            _uow.Setup(_uow => _uow.EmployeeTasksRepository).Returns(_employeeTaskRepo.Object);
            var _mediator = new Mock<IMediator>();

            //GetAll
            _mediator.Setup(x => x.Send(It.IsAny<GetAllEmployeeTasksQuery>(), CancellationToken.None)).Returns((GetAllEmployeeTasksQuery query, CancellationToken token) =>
            {
                var handler = new GetAllEmployeeTasksQueryHandler(_uow.Object, _mapper);
                Task<IEnumerable<EmployeeTask>> result = handler.Handle(query, token);
                return result;
            }
            );

            //GetbyId
            _mediator.Setup(x => x.Send(It.IsAny<GetEmployeeTaskByIdQuery>(), CancellationToken.None)).Returns((GetEmployeeTaskByIdQuery query, CancellationToken token) =>
            {
                var handler = new GetEmployeeTaskByIdQueryHandler(_uow.Object, _mapper);
                Task<EmployeeTask> result = handler.Handle(query, token);
                return result;
            }
            );


            //Create
            _mediator.Setup(x => x.Send(It.IsAny<CreateEmployeeTaskCommand>(), CancellationToken.None)).Returns((CreateEmployeeTaskCommand command, CancellationToken token) =>
            {
                var handler = new CreateEmployeeTaskCommand.CreateEmployeeTaskCommandHandler(_uow.Object, _mapper);
                Task<EmployeeTask> result = handler.Handle(command, token);
                return result;
            }
            );

            //Update
            _mediator.Setup(x => x.Send(It.IsAny<UpdateEmployeeTaskCommand>(), CancellationToken.None)).Returns((UpdateEmployeeTaskCommand command, CancellationToken token) =>
            {
                var handler = new UpdateEmployeeTaskCommand.UpdateEmployeeTaskCommandHandler(_uow.Object, _mapper);
                Task<EmployeeTask> result = handler.Handle(command, token);
                return result;
            }
            );

            //Delete
            _mediator.Setup(x => x.Send(It.IsAny<DeleteEmployeeTaskCommand>(), CancellationToken.None)).Returns((DeleteEmployeeTaskCommand command, CancellationToken token) =>
            {
                var handler = new DeleteEmployeeTaskCommand.DeleteEmployeeTaskCommandHandler(_uow.Object, _mapper);
                Task<int> result = handler.Handle(command, token);
                return result;
            }
            );
            return _mediator;
        }
    }
}
