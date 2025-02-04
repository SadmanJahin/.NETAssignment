import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { UserService } from '../users/services/user.service';

export const userApiInterceptor: HttpInterceptorFn = (req, next) => {
  const service = inject(UserService);
  const customHeaders = service.getHeader();
  const modifiedReq = req.clone({
    headers: customHeaders
  });
  return next(modifiedReq);
};
