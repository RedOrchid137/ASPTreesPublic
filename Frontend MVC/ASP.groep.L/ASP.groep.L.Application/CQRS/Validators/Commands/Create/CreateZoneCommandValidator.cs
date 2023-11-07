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
    public class CreateZoneCommandValidator : AbstractValidator<CreateZoneCommand>
    {
        public CreateZoneCommandValidator()
        {
            RuleFor(e => e.Zone).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("Zone cannot be NULL")
                .Must(z =>
                {
                    return z.SurfaceArea is not null;
                })
                .WithMessage("Surface Area for this zone cannot be NULL")
                 .Must(t =>
                 {
                     var Id = t.TreeSpeciesId;
                     int count = 0;
                     t.Site.Zones.ToList().ForEach(z =>
                     {
                         if (z.TreeSpeciesId == Id)
                         {
                             count++;
                         }
                     });
                     return count < 3;
                 })
                .WithMessage("Tree can only belong to 3 zones in the same site");
        }
    }
}
