export function formatDate(date?: Date) :string {
    if (!date) return "-"

    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };

    return date.toLocaleString(undefined, options);
}

export function CalculatePageCount(totalItems: number, pageSize: number): number {
    if (pageSize <= 0) return 1;
    return Math.ceil(totalItems / pageSize);
}