using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatus
{

    /// <summary>
    /// Validator for <see cref="ChangeOfficeStatusCommand"/>
    /// </summary>
    public sealed class ChangeOfficeStatusCommandValidator : AbstractValidator<ChangeOfficeStatusCommand>
    {
        public ChangeOfficeStatusCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("Choose a valid office status.");
        }

    }
}
