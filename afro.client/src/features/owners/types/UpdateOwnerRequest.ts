export interface UpdateOwnerRequest{
    ownerId: string;
    fullName: string;
    phoneNumber: string;
    email?: string;
    address?: string;
    notes?: string;
}
