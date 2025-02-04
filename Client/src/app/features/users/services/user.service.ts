import { inject, Injectable } from '@angular/core';
import { ApiService } from '../../../core/providers/api/api';
import { USER_ENDPOINTS } from '../constants/user-endpoints';
import { Observable } from 'rxjs';
import { UserDto } from '../models/user';
import { PageRequest, PageResponse } from '../../../core/models/page';
import { HttpHeaders } from '@angular/common/http';
import { StorageType } from '../../../core/enums/storage-type';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiService = inject(ApiService);
  private header: HttpHeaders = new HttpHeaders();
 
  getUsers(): Observable<UserDto[]> {
    return this.apiService.getForBasicApi(USER_ENDPOINTS.GET_USERS_V1);
  }

  countUsers(pageRequest: PageRequest): Observable<number> {
    return this.apiService.postForBasicApi<number>(USER_ENDPOINTS.COUNT_USERS_V1, pageRequest);
  }

  searchUsers(pageRequest: PageRequest): Observable<PageResponse<UserDto>> {
    return this.apiService.postForBasicApi<PageResponse<UserDto>>(USER_ENDPOINTS.SEARCH_USERS_V1, pageRequest);
  }

  getUserById(id: number): Observable<UserDto> {
    return this.apiService.getForBasicApi(USER_ENDPOINTS.GET_BY_ID_USER_V1(id));
  }

  createUser(user: UserDto): Observable<any> {
    return this.apiService.postForBasicApi(USER_ENDPOINTS.CREATE_USERS_V1, user);
  }

  updateUser(user: UserDto): Observable<void> {
    return this.apiService.putForBasicApi<void>(USER_ENDPOINTS.UPDATE_USERS_V1, user);
  }

  deleteUser(id: number): Observable<void> {
    return this.apiService.deleteForBasicApi<void>(USER_ENDPOINTS.DELETE_BY_ID_USER_V1(id));
  }

  setHeader(type: StorageType){
    this.header = new HttpHeaders().set('Storage-Type-Header', type.toString());
  }

  getHeader(): HttpHeaders {
    return this.header;
  }
}
