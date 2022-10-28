using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseInstructionResult
{
    public abstract Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken);

    protected TResponse ErrorResult(ErrorCode errorCode)
    {
        return (TResponse)Activator.CreateInstance(typeof(TResponse), errorCode);
    }
}