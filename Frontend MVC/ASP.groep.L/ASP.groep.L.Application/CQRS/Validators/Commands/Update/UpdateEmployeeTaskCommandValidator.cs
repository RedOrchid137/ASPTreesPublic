using ASP.groep.L.Application.CQRS.Commands;
using ASP.groep.L.Application.Interfaces;
using ASP.groep.L.Domain;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.CQRS.Validators.Commands.Update
{
    public class UpdateEmployeeTaskCommandValidator : AbstractValidator<UpdateEmployeeTaskCommand>
    {
        public UpdateEmployeeTaskCommandValidator()
        {

            RuleFor(e => e.EmployeeTask)
                .NotNull()
                .WithMessage("EmployeeTask cannot be NULL")
                .Must(s =>
                {
                    if (s.Status == Status.In_Progress)
                    {
                        return s.WorkSchedule.EmployeeTasks.Where(t => t.Status == Status.In_Progress).Count() == 0;
                    }
                    return true;
                })
                .WithMessage("Employee can only work on 1 task at a time.");


            RuleFor(e => e)
                .Must(e =>
                {
                    if (e.EmployeeTask.Status == Status.In_Progress)
                    {
                        return e.PreviousVersion.Status == Status.Todo;
                    }
                    return true;
                })
                .WithMessage("Task was already Completed.")
                .Must(e =>
                {
                    if (e.EmployeeTask.Status == Status.In_Progress)
                    {
                        return e.PreviousVersion.Status == Status.Todo;
                    }
                    return true;
                })
                .WithMessage("Task can only be started once")
                .Must(e =>
                {
                    if (e.EmployeeTask.Status == Status.Done)
                    {
                        return e.PreviousVersion.Status == Status.In_Progress;
                    }
                    return true;
                })
                .WithMessage("Task wasn't started yet.");
            RuleFor(e => e.EmployeeTask.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name cannot be NULL");

        }
    }
}
