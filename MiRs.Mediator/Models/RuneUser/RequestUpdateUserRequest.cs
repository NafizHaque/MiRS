using MediatR;
using MiRs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Mediator.Models.RuneUser
{
    /// <summary>
    /// The request for updating and retrieving Users latest data point.
    /// </summary>
    public class RequestUpdateUserRequest : IRequest<RequestUpdateUserResponse>, IValidatable
    {
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; } = string.Empty;


        /// <summary>
        /// Validates the provided store number.
        /// </summary>
        /// <exception cref="BadRequestException"> The custom exception type for bad requests.</exception>
        public void Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new BadRequestException("Username is null or Empty!");
            }
        }
    }
}
