import { inject, Injectable } from '@angular/core';
import { ApiService } from '../../../core/providers/api/api';
import { USER_ENDPOINTS } from '../constants/user-endpoints';
import { Observable } from 'rxjs';
import { UserDto } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiService = inject(ApiService);


  getUsers(): Observable<UserDto[]> {
    return this.apiService.getForBasicApi(USER_ENDPOINTS.GET_USERS_V1);
  }
}
