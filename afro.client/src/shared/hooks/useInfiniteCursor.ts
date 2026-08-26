import { useCallback, useState } from "react";
import type { CursorPagedResponse } from "../pagination";
import type { InfiniteCursorOptions } from "../pagination/InfiniteCursorOptions";

export function useInfiniteCursor<T>(options: InfiniteCursorOptions<T>){
    const [items, setItems] = useState<T[]>([]);
    const [nextCursor, setNextCursor] = useState<string | null>(null);
    const [hasMore, setHasMore] = useState(true);

    const consumePage = useCallback((page: CursorPagedResponse<T>) => {
        const pageItems = Array.isArray(page?.items) ? page.items : [];

        setItems((previous : T[])  => {
            const map = new Map<string, T>();
           // previous.forEach(item => map.set(options.getKey(item), item));
            for (const item of Array.isArray(previous) ? previous : []) {
                map.set(options.getKey(item), item);
            }
            for (const item of pageItems) {
                map.set(options.getKey(item), item);
            }

            //page.items.forEach(item => map.set(options.getKey(item), item));

            return Array.from(map.values());
        });
        setNextCursor(page.nextCursor ?? null);
        setHasMore(page.hasMore);
    }, [options]);


    const replacePage = useCallback((page: CursorPagedResponse<T>) => {
        setItems(Array.isArray(page?.items) ? page.items : []);
        setNextCursor(page.nextCursor ?? null);
        setHasMore(page.hasMore);
    }, []);


    const clear = useCallback(() => {
        setItems([]);
        setNextCursor(null);
        setHasMore(true);
    }, []);


    return {
        items,
        hasMore,
        nextCursor,
        consumePage,
        replacePage,
        clear
    };
}