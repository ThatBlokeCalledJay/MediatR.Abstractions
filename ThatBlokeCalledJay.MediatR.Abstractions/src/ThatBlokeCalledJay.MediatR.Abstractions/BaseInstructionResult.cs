using System;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseInstructionResult : IProblemInformation
{
    
    public bool HasError { get; }
    public ErrorCode ErrorCode { get; }
    
    public BaseInstructionResult()
    { }

    public BaseInstructionResult(ErrorCode errorCode)
    {
        HasError = true;
        ErrorCode = errorCode ?? throw new ArgumentNullException(nameof(errorCode));
    }
}