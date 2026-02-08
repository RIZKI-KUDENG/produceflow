import { getAssets } from "@/services/assetsService";
import {useQuery} from "@tanstack/react-query";
import type { Asset } from "@/types/assets/asset";

export const useAssets = () => {
    return useQuery<Asset[]>({
        queryKey: ['assets'],
        queryFn: getAssets,
    })
}