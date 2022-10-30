using System;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public class RequestResult : BaseRequestResult
{
    public RequestResult()
    { }

    public RequestResult(ErrorCode errorCode) : base(errorCode)
    { }
    
    public void Switch(Action<ErrorCode> onError)
    {
        if (this.HasError)
            onError(this.ErrorCode);
    }

    public TResult Match<TResult>(Func<IErrorInformation, TResult> onError)
        => onError(this);

    public static RequestResult SuccessResult() => new RequestResult();
}

public class RequestResult<TValue> : BaseRequestResult
{
    public TValue Result { get; }

    public RequestResult(TValue result)
    {
        Result = result;
    }

    public RequestResult()
    { }

    public RequestResult(ErrorCode errorCode) : base(errorCode)
    { }
    
    public void Switch(Action<TValue> onValue, Action<IErrorInformation> onError)
    {
        if (this.HasError)
            onError(this);
        else
            onValue(this.Result);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<IErrorInformation, TResult> onError)
        => this.HasError ? onError(this) : onValue(this.Result);

    public static implicit operator RequestResult<TValue>(TValue result) => new(result);

    public static RequestResult<TValue> SuccessResult(TValue result) => new(result);
}