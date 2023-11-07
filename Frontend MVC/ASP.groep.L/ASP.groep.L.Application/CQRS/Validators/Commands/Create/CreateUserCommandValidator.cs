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
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.User)
                .NotNull()
                .WithMessage("User cannot be NULL");

            RuleFor(e => e.User.FirstName)
                .NotNull()
                .WithMessage("FirstName cannot be NULL");
            
            RuleFor(e => e.User.LastName)
                .NotNull()
                .WithMessage("LastName cannot be NULL");
        }
    }
}
