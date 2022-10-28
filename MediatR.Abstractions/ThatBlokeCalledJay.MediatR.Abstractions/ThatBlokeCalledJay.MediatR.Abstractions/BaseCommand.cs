namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class BaseCommand : IRequest<InstructionResult>
{ }

public abstract class BaseCommand<T> : IRequest<InstructionResult<T>>
{ }

