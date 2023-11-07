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

namespace ASP.groep.L.Application.CQRS.Validators.Commands.Create
{
    public class CreateEmployeeTaskCommandValidator : AbstractValidator<CreateEmployeeTaskCommand>
    {
        public CreateEmployeeTaskCommandValidator()
        {
            RuleFor(e => e.EmployeeTask).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .Must(t =>
                {
                    return t.Status == Status.Todo;
                })
                .WithMessage("Status has to be TODO initially");
        }
    }
}
