import { Button } from "../ui/button";
import { Input } from "../ui/input";
import { FieldGroup, FieldLabel, Field } from "../ui/field";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
  SelectGroup,
} from "../ui/select";
import { Popover, PopoverTrigger, PopoverContent } from "../ui/popover";
import {
  Command,
  CommandInput,
  CommandList,
  CommandItem,
  CommandEmpty,
} from "../ui/command";
import { Check } from "lucide-react";
import { useState, useEffect } from "react"; 
import { useCategories } from "@/hooks/Categories/useCategories";
import type { Asset } from "@/types/assets/asset";
import { useUsers } from "@/hooks/Users/useUsers";
import { useDebounce } from "@/hooks/useDebounce";
import { useAssetStatus } from "@/hooks/Assets/useAssetStatus";
import type { AssetStatus } from "@/constans/assetStatus";
import { useUpdateAsset } from "@/hooks/Assets/useUpdateAsset";
import { useCreateAsset } from "@/hooks/Assets/useCreateAsset"; 
import type { UpdateAssetData } from "@/types/assets/updateAssetData";
import type { CreateAssetData } from "@/types/assets/createAssetData";

type props = {
  asset: Asset | null; 
  onClose: () => void;
  isOpen: boolean; 
};

export default function AssetModal({ asset, onClose, isOpen }: props) {
  const isEditMode = !!asset;

  //  Hooks
  const [holderId, setHolderId] = useState<string>("");
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 1000);
  

  const { mutate: updateAsset } = useUpdateAsset();
  const { mutate: createAsset } = useCreateAsset(); 

  const { data: categories } = useCategories();
  const { data: users } = useUsers(debouncedSearch);
  
  //  Logic
  const { status, setStatus, statusOptions } = useAssetStatus(asset?.status || "Available");
  useEffect(() => {
    if (isOpen) {
        if (asset) {
            setHolderId(asset.currentHolderId ? String(asset.currentHolderId) : "");
        } else {
            setHolderId("");
            setSearch("");
        }
    }
  }, [isOpen, asset]);

  if (!isOpen) return null;

  const selectedHolder = holderId
    ? users?.find((u) => String(u.id) === holderId)?.fullName
    : asset?.currentHolderName; 

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);
    const commonData = {
        name: (formData.get("name") as string),
        categoryId: Number(formData.get("categoryId")),
        purchasePrice: Number(formData.get("purchasePrice")),
        locationId: 1, 
        currentHolderId: Number(holderId) || null, 
        status: status,
        purchaseDate: new Date().toISOString() 
    };

    if (isEditMode && asset) {
      const payload: UpdateAssetData = {
        ...commonData,
        name: commonData.name || asset.name,
        categoryId: commonData.categoryId || asset.categoryId,
        purchasePrice: commonData.purchasePrice || asset.purchasePrice,
        locationId: asset.locationId, 
        currentHolderId: commonData.currentHolderId ? Number(commonData.currentHolderId) : asset.currentHolderId,
        purchaseDate: asset.purchaseDate,
      };

      updateAsset({
        id: asset.id,
        assetData: payload,
      });
    } else {
      const payload: CreateAssetData = {
        ...commonData,
        serialNumber: formData.get("serialNumber") as string,
        purchaseDate: new Date().toISOString(), 
        locationId: 1, 
        currentHolderId: Number(holderId) || 0, 
      };

      createAsset(payload);
    }
    
    onClose(); 
  };

  return (
    <div className="fixed inset-0 bg-black/70 flex justify-center items-center z-50">
      <div className="bg-slate-100 p-6 rounded w-full max-w-md max-h-[90vh] overflow-y-auto">
        <div className="flex justify-between mb-4">
          <h2 className="text-2xl font-bold">
            {isEditMode ? `Edit Asset ${asset?.name}` : "Create New Asset"}
          </h2>
          <Button variant="ghost" size="sm" onClick={onClose}>
            &times;
          </Button>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <FieldGroup>
            {/* Input Name */}
            <Field>
              <FieldLabel>Name</FieldLabel>
              <Input
                required
                type="text"
                name="name"
                placeholder="Asset Name"
                defaultValue={asset?.name || ""}
              />
            </Field>

            {/* Input Serial Number */}
            {!isEditMode && (
                <Field>
                <FieldLabel>Serial Number</FieldLabel>
                <Input
                    required
                    type="text"
                    name="serialNumber"
                    placeholder="S/N: 12345ABC"
                />
                </Field>
            )}

            {/* Category Select */}
            <Field>
              <FieldLabel>Category</FieldLabel>
              <Select
                name="categoryId"
                defaultValue={asset?.categoryId ? String(asset.categoryId) : undefined}
              >
                <SelectTrigger>
                  <SelectValue placeholder="Pilih Kategori" />
                </SelectTrigger>
                <SelectContent>
                  <SelectGroup>
                    {categories?.map((category: any) => (
                      <SelectItem
                        key={category.id}
                        value={String(category.id)}
                      >
                        {category.name}
                      </SelectItem>
                    ))}
                  </SelectGroup>
                </SelectContent>
              </Select>
            </Field>

            {/* Purchase Price */}
            <Field>
              <FieldLabel>Purchase Price</FieldLabel>
              <Input
                type="number"
                name="purchasePrice"
                placeholder="0"
                defaultValue={asset?.purchasePrice || ""}
              />
            </Field>

            {/* Current Holder */}
            <Field>
              <FieldLabel>Current Holder</FieldLabel>
              <Popover>
                <PopoverTrigger asChild>
                  <Button className="w-full justify-between" variant="outline">
                    {selectedHolder ? selectedHolder : "Pilih Holder (Opsional)"}
                  </Button>
                </PopoverTrigger>
                <PopoverContent className="w-full p-0">
                  <Command>
                    <CommandInput
                      placeholder="Cari holder..."
                      value={search}
                      onValueChange={setSearch}
                    />
                    <CommandList>
                      <CommandEmpty>Tidak ditemukan</CommandEmpty>
                      {users
                        ?.filter((user) =>
                          user.fullName
                            .toLowerCase()
                            .includes(search.toLowerCase()),
                        )
                        .map((user) => (
                          <CommandItem
                            key={user.id}
                            value={user.fullName}
                            onSelect={() => {
                              setHolderId(String(user.id));
                            }}
                          >
                            {user.fullName}
                            {holderId === String(user.id) && (
                              <Check className="ml-auto h-4 w-4" />
                            )}
                          </CommandItem>
                        ))}
                    </CommandList>
                  </Command>
                </PopoverContent>
              </Popover>
            </Field>

            {/* Status */}
            <Field>
              <FieldLabel>Status</FieldLabel>
              <Select
                value={status}
                onValueChange={(val: AssetStatus) => setStatus(val)}
              >
                <SelectTrigger>
                  <SelectValue placeholder="Pilih status" />
                </SelectTrigger>
                <SelectContent>
                  <SelectGroup>
                    {statusOptions.map((s) => (
                      <SelectItem key={s.value} value={s.value}>
                        {s.label}
                      </SelectItem>
                    ))}
                  </SelectGroup>
                </SelectContent>
              </Select>
            </Field>
          </FieldGroup>
          
          <Button className="mt-4 w-full" type="submit">
            {isEditMode ? "Update Asset" : "Create Asset"}
          </Button>
        </form>
      </div>
    </div>
  );
}