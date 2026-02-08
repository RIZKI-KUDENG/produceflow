import apiClient from "@/lib/axios";
import {useQuery } from "@tanstack/react-query";

export const fetchAssets = async () => {
    return useQuery({
        queryKey: ['assets'],
        queryFn: async () => {
            const response = await apiClient.get('/assets');
            return response.data;
        }
    })
}