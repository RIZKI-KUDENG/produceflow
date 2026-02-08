import { useState } from "react";
import type {Asset} from "@/types/assets/asset";

export const useAssetModal = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedAsset, setSelectedAsset] = useState<Asset | null>(null);

    const openModal = (asset : Asset) => {
        setSelectedAsset(asset);
        setIsOpen(true);
    }
    const closeModal = () => {
        setIsOpen(false);
        setSelectedAsset(null);
    }
    
    return {
        isOpen,
        selectedAsset,
        openModal,
        closeModal,
    }
}