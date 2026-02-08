import { useAssets} from "@/hooks/Assets/useAssets";
import { Table, TableCaption, TableHeader, TableRow, TableHead, TableBody, TableCell } from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { useAssetModal } from "@/hooks/Assets/useAssetModal";
import UpdateAssetModal from "@/components/Fragments/UpdateAssetModal";


export default function AssetsPage() {
    const { data: assets, isLoading, isError } = useAssets();
    const {isOpen, openModal, closeModal, selectedAssetId} = useAssetModal();

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (isError) {
        return <div>Error loading assets.</div>;
    }
    console.log(assets);

    return (
        <div className="p-4">
            <Table > 
            <TableCaption>Assets List</TableCaption>
            <TableHeader>
                <TableRow>
                    <TableHead>Name</TableHead>
                    <TableHead>Category</TableHead>
                    <TableHead>Location</TableHead>
                    <TableHead>Holder</TableHead>
                    <TableHead>Status</TableHead>
                    <TableHead>Actions</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {assets.map((asset: any) => (
                    <TableRow key={asset.id}>
                        <TableCell>
                        {asset.name}
                        </TableCell>
                        <TableCell>{asset.categoryName}</TableCell>
                        <TableCell>{asset.locationName}</TableCell>
                        <TableCell>{asset.currentHolderName}</TableCell>
                        <TableCell>{asset.status}</TableCell>
                        <TableCell>
                            <Button className="bg-red-300" variant="outline" size="sm">Delete</Button>
                            <Button onClick={() => openModal(asset.id)}  variant="outline" size="sm" className="ml-2">Edit</Button>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
        {isOpen && selectedAssetId && (
            <UpdateAssetModal assetId={selectedAssetId!} onClose={closeModal} />
        )}
        </div>
    );
}