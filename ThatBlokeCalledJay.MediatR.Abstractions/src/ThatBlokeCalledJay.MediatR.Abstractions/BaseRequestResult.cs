using System;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseRequestResult : IErrorInformation
{
    
    public bool HasError { get; }
    public ErrorCode ErrorCode { get; }
    
    public BaseRequestResult()
    { }

    public BaseRequestResult(ErrorCode errorCode)
    {
        HasError = true;
        ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
    }
}