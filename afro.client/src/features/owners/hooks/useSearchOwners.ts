import { useEffect, useState } from "react";
import { useGetOwnersQuery, type SearchOwnerParams } from "../api/ownerApi";
import { useInfiniteCursor } from "@/shared/hooks/useInfiniteCursor";
import type { Owner } from "../types/Owner";
import { useDebounce } from "@/shared/hooks/useDebounce";

export function useSearchOwners(
    request: SearchOwnerParams
) {

    const cursor =
        useInfiniteCursor<Owner>({
            getKey: owner => owner.id
        });

    const [currentCursor, setCurrentCursor] =
        useState<string>();

    const search =
        useDebounce(request.search);

    const query = {

        ...request,

        search,

        cursor: currentCursor

    };

    const result =
        useGetOwnersQuery(query);
    
    useEffect(() => {
        if (!result.data)
            return;
        cursor.consumePage(result.data);
    }, [result.data]);

    useEffect(() => {
        cursor.clear();

        setCurrentCursor(undefined);
    }, [search, request.status]);

    const loadMore = () => {
        if (!cursor.hasMore)
            return;

        setCurrentCursor(cursor.nextCursor ?? undefined);
    }

    return {
        owners: cursor.items,
        hasMore: cursor.hasMore,
        loadMore,
        isLoading: result.isLoading,
        isFetching: result.isFetching,
        error: result.error
    }

}

// export function useSearchOwners(request: SearchOwnerParams) {

//     // -----------------------------
//     // Cursor state
//     // -----------------------------

//     const [currentCursor, setCurrentCursor] = useState<string>();

//     // -----------------------------
//     // Shared cursor manager
//     // -----------------------------

//     const cursor = useInfiniteCursor<Owner>({
//         getKey: owner => owner.id
//     });

//     // -----------------------------
//     // Debounced search
//     // -----------------------------

//     const search = useDebounce(request.search);

//     // -----------------------------
//     // Query parameters
//     // -----------------------------

//     const query = useMemo(
//         () => ({
//             search,
//             status: request.status,
//             take: request.pageSize,
//             cursor: currentCursor
//         }),
//         [
//             search,
//             request.status,
//             request.pageSize,
//             currentCursor
//         ]
//     );

//     // -----------------------------
//     // API
//     // -----------------------------

//     const {
//         data,
//         error,
//         isLoading,
//         isFetching
//     } = useGetOwnersQuery(query);

//     // -----------------------------
//     // Merge returned page
//     // -----------------------------

//     useEffect(() => {

//         if (!data)
//             return;

//         cursor.consumePage(data);

//     }, [data]);

//     // -----------------------------
//     // clear when filters change
//     // -----------------------------

//     useEffect(() => {

//         cursor.reset();

//         setCurrentCursor(undefined);

//     }, [
//         search,
//         request.status
//     ]);

//     // -----------------------------
//     // Load next page
//     // -----------------------------

//     function loadMore() {

//         if (!cursor.hasMore)
//             return;

//         if (!cursor.nextCursor)
//             return;

//         setCurrentCursor(cursor.nextCursor);
//     }

//     // -----------------------------
//     // Refresh
//     // -----------------------------

//     function refresh() {

//         cursor.reset();

//         setCurrentCursor(undefined);
//     }

//     return {

//         owners: cursor.items,

//         hasMore: cursor.hasMore,

//         isLoading,

//         isFetching,

//         error,

//         loadMore,

//         refresh
//     };
// }
