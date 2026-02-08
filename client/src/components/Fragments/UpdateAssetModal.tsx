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
import type { UserLookUp } from "@/types/users/userLookUp";
import { useDebounce } from "@/hooks/useDebounce";

type props = {
  asset: Asset | null;
  onClose: () => void;
};
export default function UpdateAssetModal({ asset, onClose }: props) {
  const [holderId, setHolderId] = useState<string>("");
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 1000);
  if (!asset) return null;
  const { data: categories } = useCategories();
  const { data: users } = useUsers(debouncedSearch);
  const selectedCategory = categories?.find(
    (c: any) => c.name === asset.categoryName,
  );
  const selectedHolder =   holderId
    ? users?.find((u) => String(u.id) === holderId)?.fullName
    : asset.currentHolderName;
  console.log(users);
  return (
    <div className="fixed inset-0 bg-black/70 flex justify-center items-center z-50">
      <div className="bg-slate-100 p-6 rounded w-full max-w-md">
        <div className="flex justify-between">
          <h2 className="text-2xl">Edit Asset {asset.name}</h2>
          <Button variant="ghost" size="sm" onClick={onClose}>
            &times;
          </Button>
        </div>
        <form action="">
          <FieldGroup>
            <Field>
              <FieldLabel>Name</FieldLabel>
              <Input
                type="text"
                placeholder="Asset Name"
                defaultValue={asset.name}
              />
            </Field>
            <Field>
              <FieldLabel>Category</FieldLabel>
              <Select
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
          </FieldGroup>
        </form>
      </div>
    </div>
  );
}
