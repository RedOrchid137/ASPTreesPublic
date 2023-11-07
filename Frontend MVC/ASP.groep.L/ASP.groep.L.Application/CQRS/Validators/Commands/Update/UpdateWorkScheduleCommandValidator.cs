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
    public class UpdateWorkScheduleCommandValidator : AbstractValidator<UpdateWorkScheduleCommand>
    {
        public UpdateWorkScheduleCommandValidator()
        {
            this.CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(e => e.WorkSchedule)
                .NotNull()
                .WithMessage("WorkSchedule cannot be NULL")
                .DependentRules(() =>
                {
                    RuleFor(e => e.WorkSchedule).Cascade(CascadeMode.StopOnFirstFailure)
                        .Must(e =>
                        {
                            if (e.EmployeeTasks.Count() == 0)
                            {
                                return true;
                            }
                            return Math.Abs((e.EmployeeTasks.OrderBy(e => e.scheduledStart.Day).Last().scheduledStart - e.EmployeeTasks.OrderBy(e => e.scheduledStart.Day).First().scheduledStart).TotalDays) < 7;
                        }
                        )
                        .WithMessage("Work Schedule has to contain days within the same week")
                        .Must((e) =>
                        {
                            var days = new List<DayOfWeek>();
                            e.EmployeeTasks.ToList().ForEach(e =>
                            {
                                days.Add(e.scheduledStart.DayOfWeek);
                            });
                            return days.Distinct().Count() <= 5;
                        })
                        .WithMessage("Work Schedule can only contain 5 workdays")
                        .Must(w =>
                        {
                            bool ruleViolation = false;

                            var zones = new List<int>();
                            w.EmployeeTasks.ToList().ForEach(e =>
                            {
                                zones.Add(e.ZoneId);
                            });
                            zones = zones.Distinct().ToList();

                            var days = new List<DayOfWeek>();
                            w.EmployeeTasks.ToList().ForEach(e =>
                            {
                                days.Add(e.scheduledStart.DayOfWeek);
                            });
                            days = days.Distinct().ToList();

                            days.ForEach(d =>
                            {
                                var tasks = w.EmployeeTasks.Where(t => t.scheduledStart.DayOfWeek == d);
                                zones.ForEach(zone =>
                                {
                                    var checkList = tasks.Where(t => t.ZoneId == zone);
                                    if (checkList.Count() == 0)
                                    {
                                        ruleViolation = true;
                                    }
                                });
                            });
                            return !ruleViolation;
                        })
                        .WithMessage("Each zone has to be scheduled at least once every day");

                    RuleFor(e => e.WorkSchedule.EmployeeTasks).Cascade(CascadeMode.Stop)
                        .NotNull()
                        .Must(e =>
                        {
                            bool violation = false;
                            foreach (var item in e)
                            {
                                violation = String.IsNullOrEmpty(item.Name);
                            }
                            return !violation;
                        })
                        .WithMessage("Task Name cannot be NULL")
                        .Must(e =>
                        {
                            bool ruleViolation = false;
                            foreach (var task in e)
                            {
                                if (Math.Abs((task.scheduledStop - task.scheduledStart).TotalHours) > 8)
                                {
                                    ruleViolation = true;
                                }
                            }
                            return !ruleViolation;
                        })
                        .WithMessage("Employee can only be assigned tasks of up to 8 hours")
                        .Must(e =>
                        {
                            bool ruleViolation = false;
                            foreach (var task in e)
                            {
                                var Id = task.Zone.SiteId;

                                task.WorkSchedule.EmployeeTasks.ToList().ForEach(task =>
                                {
                                    if (task.Zone.SiteId != Id)
                                    {
                                        ruleViolation = true;
                                    }
                                });

                            }
                            return !ruleViolation;
                        })
                        .WithMessage("Employee can only be employed at 1 site per week")
                        .Must(e =>
                        {
                            bool ruleViolation = false;
                            foreach (var task in e)
                            {
                                int count = 0;
                                var day = task.scheduledStart.DayOfWeek;

                                task.WorkSchedule.EmployeeTasks.ToList().ForEach((task) =>
                                {
                                    if (task.scheduledStart.DayOfWeek == day)
                                    {
                                        count++;
                                    }
                                });
                                ruleViolation = count > 4;
                            }
                            return !ruleViolation;
                        })
                        .WithMessage("Employee can only have 4 tasks in a single workday")
                        .Must(e =>
                        {
                            bool ruleViolation = false;
                            foreach (var task in e)
                            {
                                int employeeId = task.WorkSchedule.EmployeeId;
                                var zoneTasks = task.Zone.EmployeeTasks.ToList();
                                zoneTasks.ForEach(task =>
                                {
                                    if (task.WorkSchedule.EmployeeId != employeeId && task.scheduledStart.Day == task.scheduledStart.Day)
                                    {
                                        ruleViolation = true;
                                    }
                                });
                            }

                            return !ruleViolation;
                        })
                        .WithMessage("Only one employee can be assigned to each zone every day");
                    RuleFor(e => e.WorkSchedule.PlannerId)
                        .NotNull()
                        .WithMessage("Planner cannot be NULL");

                    RuleFor(e => e.WorkSchedule.EmployeeId)
                        .NotNull()
                        .WithMessage("Employee cannot be NULL");
                });
        }
    }
}
