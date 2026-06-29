import { useOwners } from "../hooks/useOwners";
import { OwnerTable } from "../components/OwnerTable";
import { LoadingSpinner } from "@/@/components/shared/LoadingSpinner";
import { Button } from "@/@/components/ui/button";
import { EmptyState } from "@/@/components/shared/EmptyState";
import { Link } from "react-router-dom";
import { PageHeader } from "@/@/components/shared/PageHeader";
import { PageContainer, SearchInput } from "@/@/components/shared";
import { Plus } from "@/@/components/icons";

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

            <SearchInput 
                value={search}
                onChange={setSearch}
                placeholder="Search owners..."
            />

           {isLoading ? (
               <LoadingSpinner size={48} />
           ): owners.length === 0 ? (
               <EmptyState 
                   title="No Owner Found"
                   description="Create your first owner"
                   action={
                       <Button>
                           <Link to="/owners/create">
                               Create Owner
                           </Link>
                       </Button>
                   }
               />
           ): (
               <OwnerTable owners={owners} />
           )}

        </PageContainer>
        
        
    )
}