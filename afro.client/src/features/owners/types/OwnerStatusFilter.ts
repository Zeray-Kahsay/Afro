
export const OwnerStatusFilter = {
    Active: 1,
    Archived: 2,
    All: 3,
} as const;

export type OwnerStatusFilterType = typeof OwnerStatusFilter[keyof typeof OwnerStatusFilter];