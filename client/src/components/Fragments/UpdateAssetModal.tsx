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
import { useState } from "react";
import { useCategories } from "@/hooks/Categories/useCategories";
import type { Asset } from "@/types/assets/asset";
import { useUsers } from "@/hooks/Users/useUsers";
import { useDebounce } from "@/hooks/useDebounce";
import { useAssetStatus } from "@/hooks/Assets/useAssetStatus";
import type { AssetStatus } from "@/constans/assetStatus";
import { useUpdateAsset } from "@/hooks/Assets/useUpdateAsset";
import type { UpdateAssetData } from "@/types/assets/updateAssetData";

type props = {
  asset: Asset | null;
  onClose: () => void;
};
export default function UpdateAssetModal({ asset, onClose }: props) {
  const [holderId, setHolderId] = useState<string>("");
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 1000);
  const { mutate: updateAsset } = useUpdateAsset();
  if (!asset) return null;
  const { data: categories } = useCategories();
  const { data: users } = useUsers(debouncedSearch);
  const { status, setStatus, statusOptions } = useAssetStatus(asset.status);
  const selectedCategory = categories?.find(
    (c: any) => c.name === asset.categoryName,
  );
  const selectedHolder = holderId
    ? users?.find((u) => String(u.id) === holderId)?.fullName
    : asset.currentHolderName;
 const handleSubmit = (e: any) => {
  e.preventDefault();
  const formData = new FormData(e.currentTarget);

  const payload: UpdateAssetData = {
    name: (formData.get("name") as string) || asset.name,
    categoryId: Number(formData.get("categoryId")) || asset.categoryId,
    purchasePrice:
      Number(formData.get("purchasePrice")) || asset.purchasePrice,
    locationId: asset.locationId,
    currentHolderId: Number(holderId) || asset.currentHolderId,
    purchaseDate: asset.purchaseDate,
    status: status,
  };

  updateAsset({
    id: asset.id,
    assetData: payload,
  });
};


  return (
    <div className="fixed inset-0 bg-black/70 flex justify-center items-center z-50">
      <div className="bg-slate-100 p-6 rounded w-full max-w-md">
        <div className="flex justify-between">
          <h2 className="text-2xl">Edit Asset {asset.name}</h2>
          <Button variant="ghost" size="sm" onClick={onClose}>
            &times;
          </Button>
        </div>
        <form action="" onSubmit={handleSubmit}>
          <FieldGroup>
            <Field>
              <FieldLabel>Name</FieldLabel>
              <Input
                type="text"
                name="name"
                placeholder="Asset Name"
                defaultValue={asset.name}
              />
            </Field>
            <Field>
              <FieldLabel>Category</FieldLabel>
              <Select
                name="categoryId"
                defaultValue={
                  selectedCategory ? String(selectedCategory.id) : undefined
                }
              >
                <SelectTrigger>
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  <SelectGroup>
                    {categories &&
                      categories.map((category: any) => (
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
            <Field>
              <FieldLabel>Purchase Price</FieldLabel>
              <Input
                type="number"
                name="purchasePrice"
                placeholder="Asset Name"
                defaultValue={asset.purchasePrice}
              />
            </Field>
            <Field>
              <FieldLabel>Current Holder</FieldLabel>
              <Popover>
                <PopoverTrigger asChild>
                  <Button className="w-full justify-between" variant="outline">
                    {selectedHolder ? selectedHolder : "Pilih Holder"}
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
            Save
          </Button>
        </form>
      </div>
    </div>
  );
}
