import type { PageRequest } from "@/models/common/requests/PageRequest"
import type { SortRequest } from "@/models/common/requests/SortRequest"

export interface GridRequest {
    paging: PageRequest
    sorts: SortRequest[]
    search?: string
}