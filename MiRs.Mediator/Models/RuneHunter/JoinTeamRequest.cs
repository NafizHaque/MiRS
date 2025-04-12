﻿using MediatR;
using MiRs.Domain.Exceptions;

namespace MiRs.Mediator.Models.RuneHunter
{
    public class JoinTeamRequest : IRequest<JoinTeamResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the userid.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the teamName.
        /// </summary>
        public string Teamname { get; set; }

        /// <summary>
        /// Validates the user
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (UserId <= 0)
            {
                throw new BadRequestException("Invalid Id given!");
            }

            if (string.IsNullOrEmpty(Teamname))
            {
                throw new BadRequestException("Username is null or Empty!");
            }
        }
    }
}
