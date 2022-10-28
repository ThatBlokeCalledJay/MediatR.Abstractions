using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, InstructionResult<TResponse>> where TRequest : IRequest<InstructionResult<TResponse>>
{
    public abstract Task<InstructionResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    protected InstructionResult<TResponse> SuccessResult(TResponse value)
    {
        return (InstructionResult<TResponse>)Activator.CreateInstance(typeof(InstructionResult<TResponse>), value);
    }

    protected InstructionResult<TResponse> ErrorResult(ErrorCode errorCode)
    {
        return (InstructionResult<TResponse>)Activator.CreateInstance(typeof(InstructionResult<TResponse>), errorCode);
    }
}

public abstract class BaseRequestHandler<TRequest> : IRequestHandler<TRequest, InstructionResult> where TRequest : IRequest<InstructionResult>
{
    public abstract Task<InstructionResult> Handle(TRequest request, CancellationToken cancellationToken);

    protected InstructionResult SuccessResult()
    {
        return InstructionResult.SuccessResult();
    }

    protected InstructionResult ErrorResult(ErrorCode errorCode)
    {
        return (InstructionResult)Activator.CreateInstance(typeof(InstructionResult), errorCode);
    }
}