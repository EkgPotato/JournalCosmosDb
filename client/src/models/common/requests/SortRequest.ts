import type { SortOrderEnum } from "@/models/common/SortOrderEnum";

export interface SortRequest {
    field: string,
    order: SortOrderEnum,
}