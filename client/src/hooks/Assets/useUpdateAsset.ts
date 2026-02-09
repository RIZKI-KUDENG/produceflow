import { UpdateAsset } from "@/services/assetsService";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { UpdateAssetData } from "@/types/assets/updateAssetData";

export const useUpdateAsset = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, assetData }: { id: number; assetData: UpdateAssetData }) => UpdateAsset(id, assetData),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['assets'] });
        },
    });
}