namespace ThatBlokeCalledJay.MediatR.Abstractions;

public interface IErrorInformation
{
    public bool HasError { get; }

    public ErrorCode ErrorCode { get; }
}
