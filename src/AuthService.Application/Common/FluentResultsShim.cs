using System;
using System.Collections.Generic;

namespace FluentResults;

public class Result
{
    protected Result(bool isSuccess, IReadOnlyList<string> errors, IReadOnlyList<Exception> exceptions)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Exceptions = exceptions;
    }

    public bool IsSuccess { get; }
    public bool IsFailed => !IsSuccess;
    public IReadOnlyList<string> Errors { get; }
    public IReadOnlyList<Exception> Exceptions { get; }

    public static Result Ok() =>
        new(true, Array.Empty<string>(), Array.Empty<Exception>());

    public static Result Fail(string error) =>
        new(false, new[] { error }, Array.Empty<Exception>());

    public static Result Fail(Exception exception) =>
        new(false, Array.Empty<string>(), new[] { exception });

    public static Result<T> Ok<T>(T value) =>
        Result<T>.Ok(value);

    public static Result<T> Fail<T>(string error) =>
        Result<T>.Fail(error);

    public static Result<T> Fail<T>(Exception exception) =>
        Result<T>.Fail(exception);
}

public sealed class Result<T> : Result
{
    private Result(bool isSuccess, T value, IReadOnlyList<string> errors, IReadOnlyList<Exception> exceptions)
        : base(isSuccess, errors, exceptions)
    {
        Value = value;
    }

    public T Value { get; }

    public static Result<T> Ok(T value) =>
        new(true, value, Array.Empty<string>(), Array.Empty<Exception>());

    public static Result<T> Fail(string error) =>
        new(false, default!, new[] { error }, Array.Empty<Exception>());

    public static Result<T> Fail(Exception exception) =>
        new(false, default!, Array.Empty<string>(), new[] { exception });
}

