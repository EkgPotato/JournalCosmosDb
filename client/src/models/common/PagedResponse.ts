export interface PagedResponse<T> {
    items: T[],
    totalItemsCount: number
}