export interface PageRequest {
    filters: Filter[] | null;
    sorts: Sort[] | null;
    pagination: Pagination | null;
}

export interface Filter {
    propertyName: string;
    value: string;
}

export interface Sort {
    propertyName: string;
    isAscending: boolean;
}

export interface Pagination {
    pageNo: number;
    pageSize: number;
}
export interface PageResponse<T> {
    listResponseData: T[] | null;
    resultCount: number;
    hasNext: boolean | null;
}