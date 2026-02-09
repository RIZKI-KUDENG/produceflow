import apiClient from "@/lib/axios";
import type { UpdateAssetData } from "@/types/assets/updateAssetData";
import type { CreateAssetData } from "@/types/assets/createAssetData";



export const getAssets = async () => {
    const response = await apiClient.get('/api/assets');
    return response.data;
}
export const UpdateAsset = async (id: number, assetData: UpdateAssetData) => {
    const response = await apiClient.put(`/api/assets/${id}`, assetData);
    return response.data;
}
export const createAsset = async (assetData: CreateAssetData) => {
    const response = await apiClient.post('/api/assets', assetData);
    return response.data;
}