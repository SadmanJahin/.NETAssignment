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
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabel } from 'primeng/floatlabel';
import { TagModule } from 'primeng/tag';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { StorageType } from '../../../../core/enums/storage-type';
import { DropdownChangeEvent, DropdownModule } from 'primeng/dropdown';

@Component({
  selector: 'app-users-list',
  imports: [TableModule, ButtonModule, Paginator, CamelToTitleCasePipe, FormsModule, InputTextModule, FloatLabel, TagModule, DropdownModule],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent {
  private userService = inject(UserService);
  private router = inject(Router);
  dataSources = [{ name: 'SQL Database', value: StorageType.DB }, { name: 'JSON File Database', value: StorageType.JSON }];
  data: UserDto[] = []
  columns: string[] = [];
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
  isLoading: boolean = false;

  ngOnInit(): void {
    this.setHeaders();
    this.setColumns();
    this.setFilters();
    this.searchUsers();
  }

  createNewUser(): void {
    this.router.navigate(['create-user']);
  }

  setHeaders(): void {
    this.userService.setHeader(StorageType.DB);
  }

  async searchUsers(): Promise<void> {
    this.resetPagination();
    try {
      await Promise.all([this.getUsersCount(), this.getUsersData()]);
    }
    catch (ex) {
      this.isLoading = false;
    }
  }

  async getUsersCount(): Promise<void> {
    this.isLoading = true;
    const request: PageRequest = {
      filters: this.getfiltersWithoutEmptyValues(),
      sorts: null,
      pagination: null
    };
    const count: number = await firstValueFrom(this.userService.countUsers(request));
    this.totalCount = count;
    this.isLoading = false;
  }

  async getUsersData(): Promise<void> {
    this.isLoading = true;
    const request: PageRequest = {
      filters: this.getfiltersWithoutEmptyValues(),
      sorts: null,
      pagination: this.paginationInfo
    };

    const resp: PageResponse<UserDto> = await firstValueFrom(this.userService.searchUsers(request));
    this.data = resp.listResponseData ?? [];
    this.isLoading = false;
  }

  setColumns(): void {
    this.columns = Object.keys(new UserDto()).filter(item => !this.ignoredColumns.some(igCol => igCol == item));
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

  editUser(id: number): void {
    this.router.navigate([`users/${id}`]);
  }

  resetPagination(): void {
    this.paginationInfo.pageNo = 1;
    this.paginationInfo.pageSize = 25;
  }

  resetFilter() {
    this.resetPagination();
    this.setFilters();
    this.searchUsers();
  }

  onDataSourceChange(event: DropdownChangeEvent){
    this.userService.setHeader(event.value);
    this.searchUsers();
  }
}
