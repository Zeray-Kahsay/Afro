export interface CreateOwnerRequest {
    fullName: string;
    phoneNumber: string;
    email?: string;
    address?:string;
    notes?: string;
}
