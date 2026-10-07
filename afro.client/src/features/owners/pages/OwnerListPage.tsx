import { Button } from "@/@/components/ui/button";
import { Link } from "react-router-dom";
import { PageHeader } from "@/@/components/shared/PageHeader";
import { PageContainer } from "@/@/components/shared";
import { Plus } from "@/@/components/icons";
import { DataTable, DataTableSearch, DataTableToolbar } from "@/@/components/data-table";
import { ownerColumns } from "../components/OwnerColumns";
import { OwnerStatusFilter } from "../types/OwnerStatusFilter";
import { useCallback, useState } from "react";
import type { SearchOwnerParams } from "../types/SearchOwnerParams";
import { useDebounce } from "@/shared/hooks/useDebounce";
import { useGetOwnersQuery } from "../api/ownerApi";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/@/components/ui/select";
import { InfiniteScrollTrigger } from "@/shared/components/InfiniteScrollTrigger";
import { getErrorMessage } from "@/api/errorHandler";


export function OwnerListPage(){
    const [search, setSearch] = useState<string>("");
    const [status, setStatus] = useState<typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter]>(OwnerStatusFilter.Active);
    const [cursor, setCursor] = useState<string | undefined>(undefined);
    const debouncedSearch = useDebounce(search, 300);

    const query : SearchOwnerParams = {
        search: debouncedSearch || undefined,
        status,
        cursor,
        pageSize: 20
    }

    const { data, error, isLoading, isFetching, refetch } = useGetOwnersQuery(query);
   

   const handleSearchChange = (value: string) => {
    setSearch(value);
   }

   const handleStatusChange = (value: typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter]) => {
    setStatus(value);
    setCursor(undefined);
   }

   const paginationError = data && !isLoading && error ?
                getErrorMessage(error) : undefined;

   const loadMore = useCallback(() => {
        if (isFetching){
            return;
        }

        if (!data?.hasMore || !data?.nextCursor){
            return;
        }

        setCursor(data.nextCursor);

   }, [isFetching, data?.hasMore, data?.nextCursor]);


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
                >
                    <Select
                        value={String(status)}
                        onValueChange={(value) => handleStatusChange(Number(value) as typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter])}
                    >
                        <SelectTrigger className="w-40">
                            <SelectValue placeholder="Status" />
                        </SelectTrigger>
                        <SelectContent>
                            <SelectItem value={String(OwnerStatusFilter.Active)}>Active</SelectItem>
                            <SelectItem value={String(OwnerStatusFilter.Archived)}>Archived</SelectItem>
                            <SelectItem value={String(OwnerStatusFilter.All)}>All</SelectItem>
                        </SelectContent>
                    </Select>
                </DataTableToolbar>

                <DataTable
                    columns={ownerColumns}
                    data={data?.items ?? []}
                    options={{
                        loading: isLoading,
                        emptyTitle: "No Owner Found",
                        emptyDescription: "Create your first owner",
                        
                    }}
                />
                <InfiniteScrollTrigger
                    hasMore={data?.hasMore ?? false}
                    loading={isFetching}
                    error={paginationError}
                    onLoadMore={loadMore}
                    onRetry={refetch}
                />
            </PageContainer>
        
    )
}