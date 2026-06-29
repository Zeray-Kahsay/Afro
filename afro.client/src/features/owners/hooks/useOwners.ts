import { useState } from "react";
import { useSearchOwnersQuery } from "../api/ownerApi";

export function useOwners(){
    const [search, setSearch] = useState("");

    const {data: owners = [], isLoading, isFetching, refetch} = useSearchOwnersQuery(search);

    return {
        owners,
        search,
        setSearch,
        isLoading,
        isFetching,
        refetch
    };
      
    
}