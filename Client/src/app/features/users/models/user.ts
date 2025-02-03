export class UserDto {
    id!: number;
    firstName!: string;
    lastName!: string;
    company!: string;
    gender!: string;
    active!: boolean;
    contact!: ContactDto;
    role!: RoleDto;
    
}

export class ContactDto {
    id: number;
    phone: string;
    city: string | null;
    address: string;
    country: string | null;

    constructor(
        id: number,
        phone: string,
        city: string | null,
        address: string,
        country: string | null
    ) {
        this.id = id;
        this.phone = phone;
        this.city = city;
        this.address = address;
        this.country = country;
    }
}

export class RoleDto {
    id: number;
    name: string;

    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }
}
