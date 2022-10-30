using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, RequestResult<TResponse>> where TRequest : IRequest<RequestResult<TResponse>>
{
    public abstract Task<RequestResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    protected RequestResult<TResponse> SuccessResult(TResponse value)
    {
        return (RequestResult<TResponse>)Activator.CreateInstance(typeof(RequestResult<TResponse>), value);
    }

    protected RequestResult<TResponse> ErrorResult(ErrorCode errorCode)
    {
        return (RequestResult<TResponse>)Activator.CreateInstance(typeof(RequestResult<TResponse>), errorCode);
    }
}

public abstract class BaseRequestHandler<TRequest> : IRequestHandler<TRequest, RequestResult> where TRequest : IRequest<RequestResult>
{
    public abstract Task<RequestResult> Handle(TRequest request, CancellationToken cancellationToken);

    protected RequestResult SuccessResult()
    {
        return RequestResult.SuccessResult();
    }

    protected RequestResult ErrorResult(ErrorCode errorCode)
    {
        return (RequestResult)Activator.CreateInstance(typeof(RequestResult), errorCode);
    }
}