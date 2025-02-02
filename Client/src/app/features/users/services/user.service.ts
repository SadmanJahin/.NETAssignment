import { inject, Injectable } from '@angular/core';
import { ApiService } from '../../../core/providers/api/api';
import { USER_ENDPOINTS } from '../constants/user-endpoints';
import { Observable } from 'rxjs';
import { UserDto } from '../models/user';
import { PageRequest, PageResponse } from '../../../core/models/page';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiService = inject(ApiService);


  getUsers(): Observable<UserDto[]> {
    return this.apiService.getForBasicApi(USER_ENDPOINTS.GET_USERS_V1);
  }

  countUsers(pageRequest: PageRequest): Observable<number> {
    return this.apiService.postForBasicApi<number>(USER_ENDPOINTS.COUNT_USERS_V1, pageRequest);
  }

  searchUsers(pageRequest: PageRequest): Observable<PageResponse<UserDto>> {
    return this.apiService.postForBasicApi<PageResponse<UserDto>>(USER_ENDPOINTS.SEARCH_USERS_V1, pageRequest);
  }
}
