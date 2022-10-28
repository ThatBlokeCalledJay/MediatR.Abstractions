using Shouldly;

namespace ThatBlokeCalledJay.MediatR.Abstractions.Tests;

[TestClass]
public class BaseRequestHandlerTests
{
    private const int ExpectedValue = 10;
    private const string ExpectCode = "E1";
    private const string ExpectMessage = "Aww shucks!";

    private class TestCommand : BaseCommand<int?>
    { }

    private class TestError : ErrorCode
    {
        public override string Code => ExpectCode;

        public override string ErrorMessage => ExpectMessage;
    }

    private class SuccessHandler : BaseRequestHandler<TestCommand, int?>
    {
        public override Task<InstructionResult<int?>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(SuccessResult(ExpectedValue));
        }
    }

    private class ErrorHandler : BaseRequestHandler<TestCommand, int?>
    {
        public override Task<InstructionResult<int?>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(ErrorResult(new TestError()));
        }
    }

    [TestMethod]
    public void Success_Should_Return_SuccessResult()
    {
        var cmd = new TestCommand();

        var handler = new SuccessHandler();

        var result = handler.Handle(cmd, CancellationToken.None).Result;

        result.HasError.ShouldBeFalse();
        result.Result.ShouldBe(ExpectedValue);
        result.ErrorCode.ShouldBeNull();
    }

    [TestMethod]
    public void Error_Should_Return_ErrorResult()
    {
        var cmd = new TestCommand();

        var handler = new ErrorHandler();

        var result = handler.Handle(cmd, CancellationToken.None).Result;

        result.HasError.ShouldBeTrue();
        result.Result.ShouldBeNull();
        result.ErrorCode.ShouldBeOfType<TestError>();
        result.ErrorCode.Code.ShouldBe(ExpectCode);
        result.ErrorCode.ErrorMessage.ShouldBe(ExpectMessage);
    }
}