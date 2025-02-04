import { Component, inject } from '@angular/core';
import { UserService } from '../../services/user.service';
import { UserDto } from '../../models/user';
import { HttpErrorResponse } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { CamelToTitleCasePipe } from '../../../../core/pipes/camel-to-title-case.pipe';
import { Filter, PageRequest, PageResponse, Pagination, Sort } from '../../../../core/models/page';
import { ButtonModule } from 'primeng/button';
import { Paginator, PaginatorState } from 'primeng/paginator';
import { StringUtils } from '../../../../core/models/string-utils';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-users-list',
  imports: [TableModule, ButtonModule, Paginator, CamelToTitleCasePipe, FormsModule],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent {
  private userService = inject(UserService);
  data: UserDto[] = []
  columns: string[] = []
  ignoredColumns: string[] = ['contact', 'role'];
  defaultSortBy: Sort = {
    propertyName: 'transactionDate',
    isAscending: false
  }
  paginationInfo: Pagination = {
    pageNo: 1,
    pageSize: 25,
  }
  filters: Filter[] | null = null;
  totalCount: number = 0;

  ngOnInit(): void {
    this.searchUsers();
  }

  searchUsers() {
    this.getUsersCount();
    this.getUsersData();
  }

  getUsersCount() {
    const request: PageRequest = {
      filters: this.getfiltersWithoutEmptyValues(),
      sorts: null,
      pagination: null
    }

    this.userService.countUsers(request).subscribe({
      next: (count: number) => {
        this.totalCount = count;
      },
      error: (err: HttpErrorResponse) => {

      }
    })
  }

  getUsersData() {
    const request: PageRequest = {
      filters: this.getfiltersWithoutEmptyValues(),
      sorts: null,
      pagination: this.paginationInfo
    }

    this.userService.searchUsers(request).subscribe({
      next: (resp: PageResponse<UserDto>) => {
        if (resp.listResponseData && resp.listResponseData?.length > 0) {
          this.columns = Object.keys(resp.listResponseData[0]).filter(item => !this.ignoredColumns.some(igCol => igCol == item));
          this.setFilters();
          this.data = resp.listResponseData;
        }
      },
      error: (err: HttpErrorResponse) => {

      }
    })
  }

  setFilters(): void {
    this.filters = [];
    this.columns.forEach(element => {
      const filter: Filter = {
        propertyName: element,
        value: StringUtils.Empty
      }
      this.filters?.push(filter)
    });
  }

  getfiltersWithoutEmptyValues(): Filter[] {
    return this.filters?.filter(item => !(item.value != null && item.value == StringUtils.Empty)) ?? []
  }

  pageChange(data: PaginatorState): void {
    this.paginationInfo.pageNo = (data.page || 0) + 1;
    this.paginationInfo.pageSize = data.rows || 25;
    this.getUsersData();
  }
}
