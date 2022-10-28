using System;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public class InstructionResult : BaseInstructionResult
{
    public InstructionResult()
    { }

    public InstructionResult(ErrorCode errorCode) : base(errorCode)
    { }
    
    public void Switch(Action<ErrorCode> onError)
    {
        if (this.HasError)
            onError(this.ErrorCode);
    }

    public TResult Match<TResult>(Func<IProblemInformation, TResult> onError)
        => onError(this);

    public static InstructionResult SuccessResult() => new InstructionResult();
}

public class InstructionResult<TValue> : BaseInstructionResult
{
    public TValue Result { get; }

    public InstructionResult(TValue result)
    {
        Result = result;
    }

    public InstructionResult()
    { }

    public InstructionResult(ErrorCode errorCode) : base(errorCode)
    { }
    
    public void Switch(Action<TValue> onValue, Action<IProblemInformation> onError)
    {
        if (this.HasError)
            onError(this);
        else
            onValue(this.Result);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<IProblemInformation, TResult> onError)
        => this.HasError ? onError(this) : onValue(this.Result);

    public static implicit operator InstructionResult<TValue>(TValue result) => new(result);

    public static InstructionResult<TValue> SuccessResult(TValue result) => new(result);
}