import { getAssets } from "@/services/assetsService";
import {useQuery} from "@tanstack/react-query";

export const useAssets = () => {
    return useQuery({
        queryKey: ['assets'],
        queryFn: getAssets,
    })
}