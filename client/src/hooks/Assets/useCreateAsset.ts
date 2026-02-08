import apiClient from "@/lib/axios";

export const createAsset = async (assetData: any) => {
    const response = await apiClient.post('/api/assets', assetData);
    return response.data;
}