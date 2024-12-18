﻿using System.Text.Json.Serialization;

namespace FleetManager.Domain.SeedWork.Results
{
    public class Result
    {
        [JsonConstructor]
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        public readonly T? Value;

        protected Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }

        protected Result(bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = default;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, Error.None);

        public new static Result<T> Failure(Error error) => new(false, error);
    }
}
