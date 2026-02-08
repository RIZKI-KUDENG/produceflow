import { UpdateAsset } from "@/services/assetsService";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useUpdateAsset = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, assetData }: { id: number; assetData: any }) => UpdateAsset(id, assetData),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['assets'] });
        },
    });
}

