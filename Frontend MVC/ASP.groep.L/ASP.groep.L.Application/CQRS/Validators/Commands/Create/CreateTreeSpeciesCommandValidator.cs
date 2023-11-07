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
    public class CreateTreeSpeciesCommandValidator : AbstractValidator<CreateTreeSpeciesCommand>
    {
        public CreateTreeSpeciesCommandValidator()
        {
            RuleFor(e => e.TreeSpecies)
                .NotNull()
                .WithMessage("Tree Species cannot be NULL");

            RuleFor(e => e.TreeSpecies.Name)
                .NotNull()
                .WithMessage("Name cannot be NULL");
            
            RuleFor(e => e.TreeSpecies.MaintenancePlan)
                .NotNull()
                .WithMessage("Maintenance Plan cannot be NULL");
        }
    }
}
