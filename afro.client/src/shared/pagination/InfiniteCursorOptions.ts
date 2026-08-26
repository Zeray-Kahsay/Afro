
export interface InfiniteCursorOptions<T> {
    getKey(item: T) : string;
}