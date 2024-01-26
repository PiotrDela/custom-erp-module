using MediatR;

namespace CustomERP.Trucks.Application
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
