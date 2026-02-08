import { useQuery } from "@tanstack/react-query";
import { fetchUser } from "@/services/userService";
import type { UserLookUp } from "@/types/users/userLookUp";

export const useUsers = (search?: string) => {
    return useQuery<UserLookUp[]>({
        queryKey: ['users', search],
        queryFn: () => fetchUser(search)
    })
}