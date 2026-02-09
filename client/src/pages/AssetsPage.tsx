import { useAssets } from "@/hooks/Assets/useAssets";
import {
  Table,
  TableCaption,
  TableHeader,
  TableRow,
  TableHead,
  TableBody,
  TableCell,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { useAssetModal } from "@/hooks/Assets/useAssetModal";
import UpdateAssetModal from "@/components/Fragments/UpdateAssetModal";
import type { Asset } from "@/types/assets/asset";

export default function AssetsPage() {
  const { data: assets, isLoading, isError } = useAssets();
  const { isOpen, openModal, closeModal, selectedAsset } = useAssetModal();
  function StatusBadge({ status }: { status: string }) {
  const colorMap: Record<string, string> = {
    Available: "bg-green-100 text-green-700",
    Assigned: "bg-blue-100 text-blue-700",
    Maintenance: "bg-yellow-100 text-yellow-700",
    Disposed: "bg-red-100 text-red-700",
  };

  return (
    <span
      className={`px-2 py-1 rounded-md text-xs font-medium ${
        colorMap[status] || "bg-gray-100 text-gray-700"
      }`}
    >
      {status}
    </span>
  );
}


  if (isLoading) {
    return (
      <div className="p-6">
        <div className="text-sm text-muted-foreground">Loading assets...</div>
      </div>
    );
  }

  if (isError) {
    return (
      <div className="p-6 text-red-500">
        Failed to load assets. Please try again.
      </div>
    );
  }

  return (
    <div className="p-6 max-w-7xl mx-auto space-y-6">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-semibold">Assets</h1>
          <p className="text-sm text-muted-foreground">
            Manage and monitor all company assets
          </p>
        </div>

        <Button className="px-5">+ Add Asset</Button>
      </div>

      {/* Table Card */}
      <div className="bg-white border rounded-xl shadow-sm">
        <Table>
          <TableCaption className="pb-4">
            List of registered assets
          </TableCaption>

          <TableHeader>
            <TableRow className="bg-muted/40">
              <TableHead>Name</TableHead>
              <TableHead>Category</TableHead>
              <TableHead>Location</TableHead>
              <TableHead>Holder</TableHead>
              <TableHead>Status</TableHead>
              <TableHead className="text-right pr-6">Actions</TableHead>
            </TableRow>
          </TableHeader>

          <TableBody>
            {assets?.length === 0 && (
              <TableRow>
                <TableCell colSpan={6} className="text-center py-10 text-muted-foreground">
                  No assets available
                </TableCell>
              </TableRow>
            )}

            {assets?.map((asset: Asset) => (
              <TableRow key={asset.id} className="hover:bg-muted/30 transition">
                <TableCell className="font-medium">
                  {asset.name}
                </TableCell>

                <TableCell>{asset.categoryName}</TableCell>

                <TableCell>{asset.locationName}</TableCell>

                <TableCell>
                  {asset.currentHolderName || (
                    <span className="text-muted-foreground">-</span>
                  )}
                </TableCell>

                <TableCell>
                  <StatusBadge status={asset.status} />
                </TableCell>

                <TableCell className="text-right space-x-2 pr-6">
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => openModal(asset)}
                  >
                    Edit
                  </Button>

                  <Button
                    variant="destructive"
                    size="sm"
                  >
                    Delete
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>

      {isOpen && selectedAsset && (
        <UpdateAssetModal asset={selectedAsset} onClose={closeModal} />
      )}
    </div>
  );
}
