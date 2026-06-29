import type { ReactNode } from "react";

export interface DataTableColumn<T>{
    id: string;
    header: ReactNode;
    className?: string;
    cell: (item: T) => ReactNode;
}