using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Behaviors.Messaging
{
    public interface ICommand : IRequest, ICommandBase
    {

    }
    public interface ICommand<TRequest> : IRequest<TRequest>, ICommandBase
    {

    }


    public interface ICommandBase
    {
    }
}
