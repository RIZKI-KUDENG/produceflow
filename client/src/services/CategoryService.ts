import apiClient from "@/lib/axios";

export const getCategories = async () => {
    const response = await apiClient.get('/api/category');
    return response.data;
}