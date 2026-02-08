import { useState } from "react";

export const useAssetModal = () => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedAssetId, setSelectedAssetId] = useState<number | null>(null);

    const openModal = (assetId : number) => {
        setSelectedAssetId(assetId);
        setIsOpen(true);
    }
    const closeModal = () => {
        setIsOpen(false);
        setSelectedAssetId(null);
    }
    
    return {
        isOpen,
        selectedAssetId,
        openModal,
        closeModal,
    }
}