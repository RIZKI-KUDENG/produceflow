import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createAsset } from "@/services/assetsService";
import type { CreateAssetData } from "@/types/assets/createAssetData";

export const useCreateAsset = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (assetData: CreateAssetData) => createAsset(assetData),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['assets'] });
        },
    });
}
