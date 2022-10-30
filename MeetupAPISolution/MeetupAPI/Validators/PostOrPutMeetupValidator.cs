using FluentValidation;
using MeetupAPI.DTOs;
using MeetupAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Validators
{
    public class PostOrPutMeetupValidator : AbstractValidator<MeetupDTO>
    {
        public PostOrPutMeetupValidator()
        {
            RuleFor(x => x.Topic)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fill the 'Topic' field.");

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fill the 'Description' field.");

            RuleFor(x => x.Plan)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fill the 'Plan' field.");

            RuleFor(x => x.Sponsor)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fill the 'Sponsor' field.");

            RuleFor(x => x.EventLocation)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fill the 'EventLocation' field.");

            RuleFor(x => x.EventDateTime)
                .NotNull()
                .GreaterThan(DateTime.Now)
                .WithMessage("The input date must be later than current.");

            RuleFor(x => x.Speakers)
                .NotNull()
                .NotEmpty()
                .WithMessage("At list one participant must be at the meeting.");
        }
    }
}
