import type { CursorPagedResponse } from "./CursorPagedResponse";

export interface UseInfiniteCursorResult<T> {
    items: T[];
    hasMore: boolean;
    nextCursor: string | null;
    consumePage(page: CursorPagedResponse<T>): void;
    replace(page: CursorPagedResponse<T>): void;
    reset(): void;
}