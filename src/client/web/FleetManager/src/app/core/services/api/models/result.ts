import {ApiError} from './api-error';

export class Result{
  public readonly isSuccess: boolean;
  public readonly isFailure: boolean;
  public error?: ApiError;

  constructor(isSuccess: boolean, error?: ApiError) {
    this.isSuccess = isSuccess;
    this.isFailure = !isSuccess;
    this.error = error;
  }


}

export class ValueResult<T> extends Result{
  public readonly value: T;

  constructor(value: T, isSuccess: boolean, error?: ApiError) {
    super(isSuccess, error);

    this.value = value;
  }
}
