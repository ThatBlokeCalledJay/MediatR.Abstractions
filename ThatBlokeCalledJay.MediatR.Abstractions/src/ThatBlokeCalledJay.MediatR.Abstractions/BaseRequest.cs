using MediatR;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseRequest : IRequest<RequestResult>
{ }

public abstract class BaseRequest<T> : IRequest<RequestResult<T>>
{ }

