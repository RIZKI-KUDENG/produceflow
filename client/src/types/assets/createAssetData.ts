export type CreateAssetData = {
    name: string;
    categoryId: number;
    purchasePrice: number;
    purchaseDate: string;
    locationId: number;
    currentHolderId: number;
    status: string;
    serialNumber: string
};