export interface UserDto {
    id: number;
    firstName: string;
    lastName: string;
    company: string;
    gender: string;
    active: boolean;
    contact: ContactDto;
    role: RoleDto;
}

export interface ContactDto {
    id: number;
    phone: string;
    city: string | null;
    address: string;
    country: string | null;
}

export interface RoleDto {
    id: number;
    name: string;
}