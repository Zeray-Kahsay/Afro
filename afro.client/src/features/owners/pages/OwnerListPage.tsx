import { useOwners } from "../hooks/useOwners";
import { Button } from "@/@/components/ui/button";
import { Link } from "react-router-dom";
import { PageHeader } from "@/@/components/shared/PageHeader";
import { PageContainer } from "@/@/components/shared";
import { Plus } from "@/@/components/icons";
import { DataTable, DataTableSearch, DataTableToolbar } from "@/@/components/data-table";
import { ownerColumns } from "../components/OwnerColumns";

export function OwnerListPage(){
    const {owners, search, setSearch, isLoading } = useOwners();

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
                            onChange={setSearch}      
                        />
                    }
                />

                <DataTable
                    columns={ownerColumns}
                    data={owners}
                    options={{
                        loading: isLoading,
                        emptyTitle: "No Owner Found",
                        emptyDescription: "Create your first owner",
                        
                    }}
                />
            </PageContainer>
        // <PageContainer>
        //     <PageHeader
        //        title="Owners"
        //        description="Manage property owners"
        //        action={
        //            <Button asChild>
        //                <Link 
        //                  to="/owners/create" 
        //                  className="inline-flex items-center gap-2">
        //                    <Plus className="h-4 w-4" />
        //                    New Owner
        //                </Link>
        //            </Button>
        //        }
        //     /> 

        //     <SearchInput 
        //         value={search}
        //         onChange={setSearch}
        //         placeholder="Search owners..."
        //     />

        //    {isLoading ? (
        //        <LoadingSpinner size={48} />
        //    ): owners.length === 0 ? (
        //        <EmptyState 
        //            title="No Owner Found"
        //            description="Create your first owner"
        //            action={
        //                <Button>
        //                    <Link to="/owners/create">
        //                        Create Owner
        //                    </Link>
        //                </Button>
        //            }
        //        />
        //    ): (
        //        <OwnerTable owners={owners} />
        //    )}

        // </PageContainer>
        
        
    )
}