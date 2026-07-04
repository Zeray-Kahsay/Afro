export interface Owner {
    id: string;
    fullName: string;
    phoneNumber: string;
    email?: string;
    country: string;
    city: string;
    createdAtUtc: string;
    notes?: string;
    address?: string;
}