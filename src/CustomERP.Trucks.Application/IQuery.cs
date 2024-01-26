using MediatR;

namespace CustomERP.Trucks.Application
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
