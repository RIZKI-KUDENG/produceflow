import apiClient from "@/lib/axios";


export const getAssets = async () => {
    const response = await apiClient.get('/api/assets');
    return response.data;
}