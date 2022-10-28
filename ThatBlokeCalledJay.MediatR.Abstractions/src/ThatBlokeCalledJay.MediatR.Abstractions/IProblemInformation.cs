namespace ThatBlokeCalledJay.MediatR.Abstractions;

public interface IProblemInformation
{
    public bool HasError { get; }

    public ErrorCode ErrorCode { get; }
}
