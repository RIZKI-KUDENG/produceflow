import apiClient from "@/lib/axios";
import type { UserLookUp } from "@/types/users/userLookUp";

export const fetchUser = async (search?: string): Promise<UserLookUp[]> => {
    const response = await apiClient.get("/api/users", 
        { params: { search: search?.trim()}
     });
    return response.data;
}