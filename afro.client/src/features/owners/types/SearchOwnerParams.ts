import type { CursorRequest } from "@/shared/pagination/CursorRequest";
import {  OwnerStatusFilter } from "./OwnerStatusFilter";

export interface SearchOwnerParams extends CursorRequest {
    search?: string;                            
    status?: typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter];

   
}