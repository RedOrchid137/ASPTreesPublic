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
    public class UpdateSiteCommandValidator : AbstractValidator<UpdateSiteCommand>
    {
        public UpdateSiteCommandValidator()
        {
            RuleFor(e => e.Site)
                .NotNull()
                .WithMessage("Site cannot be NULL");

            RuleFor(e => e.Site.Address)
                .NotNull()
                .WithMessage("Address cannot be NULL");
            
            RuleFor(e => e.Site.Name)
                .NotNull()
                .WithMessage("Name cannot be NULL");
        }
    }
}
