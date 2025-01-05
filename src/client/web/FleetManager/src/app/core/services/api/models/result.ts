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

  static isResult(obj: any): obj is Result {
    return (
      obj &&
      typeof obj.isSuccess === 'boolean' &&
      typeof obj.error === 'object' &&
      typeof obj.error.code === 'string' &&
      typeof obj.error.description === 'string'
    );
  }
}

export class ValueResult<T> extends Result{
  public readonly value: T;

  constructor(value: T, isSuccess: boolean, error?: ApiError) {
    super(isSuccess, error);

    this.value = value;
  }
}
