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
    public class UpdateMaintenancePlanCommandValidator : AbstractValidator<UpdateMaintenancePlanCommand>
    {
        public UpdateMaintenancePlanCommandValidator()
        {
            RuleFor(e => e.MaintenancePlan)
                .NotNull()
                .WithMessage("MaintenancePlan cannot be NULL");

            RuleFor(e => e.MaintenancePlan.Title)
                .NotNull()
                .WithMessage("Title cannot be NULL");
            
            RuleFor(e => e.MaintenancePlan.Link)
                .NotNull()
                .WithMessage("Link cannot be NULL");

            RuleFor(e => e.MaintenancePlan.Season)
                .NotNull()
                .WithMessage("Season cannot be NULL");
        }
    }
}
