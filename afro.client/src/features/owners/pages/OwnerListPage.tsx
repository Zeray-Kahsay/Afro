import { Button } from "@/@/components/ui/button";
import { Link } from "react-router-dom";
import { PageHeader } from "@/@/components/shared/PageHeader";
import { PageContainer } from "@/@/components/shared";
import { Plus } from "@/@/components/icons";
import { DataTable, DataTableSearch, DataTableToolbar } from "@/@/components/data-table";
import { ownerColumns } from "../components/OwnerColumns";
import { OwnerStatusFilter } from "../types/OwnerStatusFilter";
import { useEffect, useMemo, useState } from "react";
import { useInfiniteCursor } from "@/shared/hooks/useInfiniteCursor";
import type { SearchOwnerParams } from "../types/SearchOwnerParams";
import { useDebounce } from "@/shared/hooks/useDebounce";
import { useGetOwnersQuery } from "../api/ownerApi";
import type { Owner } from "../types/Owner";

export function OwnerListPage(){
    const [status, setStatus] = useState<typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter]>(OwnerStatusFilter.Active);
    const [cursor, setCursor] = useState<string | undefined>(undefined);
    const paginationOptions = useMemo(() => ({
        getKey: (owner: Owner) => owner.id
    }), []);
    const pagination = useInfiniteCursor<Owner>(paginationOptions);
    const [search, setSearch] = useState<string>("");
    const debouncedSearch = useDebounce(search, 300);

    const query : SearchOwnerParams = {
        search: debouncedSearch || undefined,
        status,
        cursor,
        pageSize: 20
    }

    const { data, error, isLoading, isFetching, refetch } = useGetOwnersQuery(query);

    // A response with no cursor is the first page 
    // A response with a cursor is a subsequent page
   useEffect(() => {
    if (!data){
        return;
    }

    if (cursor === undefined){
        pagination.replacePage(data);
        return;
    }
    pagination.consumePage(data);
   }, [data, cursor, pagination.consumePage, pagination.replacePage]);

   const loadMore = () => {
    if (isFetching){
        return;
    }

    if (!pagination.hasMore){
        return;
    }

    if (!pagination.nextCursor){
        return;
    }
    setCursor(pagination.nextCursor);
   }

   const handleSearchChange = (value: string) => {
    setSearch(value);
   }

   const handleStatusChange = (value: typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter]) => {
    setStatus(value);
    setCursor(undefined);
    pagination.clear();
   }

   useEffect(() => {
    setCursor(undefined);
    pagination.clear();
   }, [debouncedSearch]);

   const refresh = () => {
    setCursor(undefined);
    pagination.clear();
    refetch();  
   }

    return (
            <PageContainer>
                <PageHeader 
                    title="Owners"
                    description="Manage property owners"
                    action={
                   <Button asChild>
                       <Link 
                         to="/owners/create" 
                         className="inline-flex items-center gap-2">
                           <Plus className="h-4 w-4" />
                           New Owner
                       </Link>
                   </Button>
                }
                />
                <DataTableToolbar 
                    search={
                        <DataTableSearch
                            value={search}
                            onChange={handleSearchChange}      
                        />
                    }
                />

                <DataTable
                    columns={ownerColumns}
                    data={pagination.items}
                    options={{
                        loading: isLoading,
                        emptyTitle: "No Owner Found",
                        emptyDescription: "Create your first owner",
                        
                    }}
                />
            </PageContainer>
        
    )
}