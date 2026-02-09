import { useState } from "react";
import { ASSET_STATUS, ASSET_STATUS_OPTIONS } from "@/constans/assetStatus";
import type { AssetStatus } from "@/constans/assetStatus";

export const useAssetStatus = (initialStatus?: string) => {
    const [status, setStatus] = useState<AssetStatus>(
        initialStatus as AssetStatus || ASSET_STATUS.AVAILABLE
    )
    const getStatusLabel = (value?: string) => {
        return ASSET_STATUS_OPTIONS.find((s) => s.value === value)?.label || "-"
    }
    return {
        status,
        getStatusLabel,
        setStatus,
        statusOptions: ASSET_STATUS_OPTIONS
    }
}