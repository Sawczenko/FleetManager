import { HttpInterceptorFn } from '@angular/common/http';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');
  console.log(token)

  if (token) {
    const headers = req.headers.set('Authorization', `${token}`);
    req = req.clone({headers});
  }
  return next(req);
}
