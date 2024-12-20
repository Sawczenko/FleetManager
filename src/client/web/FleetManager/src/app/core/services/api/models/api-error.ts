export class ApiError {
  public code: string;
  public description: string;

  constructor(code: string, description: string) {
    this.code = code;
    this.description = description;
  }
}
