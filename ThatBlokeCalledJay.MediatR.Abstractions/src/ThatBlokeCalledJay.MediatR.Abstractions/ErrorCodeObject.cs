using System;
using System.Collections.Generic;
using System.Linq;

namespace ThatBlokeCalledJay.MediatR.Abstractions;

public abstract class ErrorCode
{
    private readonly List<ErrorCodeDetail> _errorDetails = new();

    public abstract string Code { get; }

    public abstract string ErrorMessage();

    public IReadOnlyCollection<ErrorCodeDetail> ErrorDetails
        => _errorDetails;

    /// <returns>The value of <see cref="Code"/> </returns>
    public override string ToString()
    {
        return Code;
    }

    public void AddErrorDetail(string key, object value)
    {
        _errorDetails.Add(new ErrorCodeDetail(key, value));
    }

    public void AddErrorDetail(ErrorCodeDetail errorDetail)
    {
        if (errorDetail == null)
            throw new ArgumentNullException(nameof(errorDetail));

        _errorDetails.Add(errorDetail);
    }

    public void AddErrorDetails(IEnumerable<ErrorCodeDetail> errorDetails)
    {
        if (errorDetails == null)
            throw new ArgumentNullException(nameof(errorDetails));

        _errorDetails.AddRange(errorDetails.ToList());
    }

    public void AddErrorDetails(IEnumerable<KeyValuePair<string, object>> errorDetails)
    {
        if (errorDetails == null)
            throw new ArgumentNullException(nameof(errorDetails));

        _errorDetails.AddRange(errorDetails.Select(detail => (ErrorCodeDetail)detail));
    }

    public static implicit operator KeyValuePair<string, string>(ErrorCode errorCode)
        => new(errorCode.Code, errorCode.ErrorMessage());
}

public class ErrorCodeDetail
{
    public string Property { get; }

    public object Value { get; }

    public ErrorCodeDetail(string property, object value)
    {
        if (string.IsNullOrWhiteSpace(property))
            throw new ArgumentNullException(nameof(property));

        Property = property;
        Value = value;
    }

    public static implicit operator KeyValuePair<string, object>(ErrorCodeDetail errorCodeObject)
        => new(errorCodeObject.Property, errorCodeObject.Value);

    public static implicit operator ErrorCodeDetail(KeyValuePair<string, object> errorDetails)
        => new(errorDetails.Key, errorDetails.Value);
}

