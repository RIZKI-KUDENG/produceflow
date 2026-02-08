import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { FieldGroup, FieldLabel, Field } from "../ui/field"
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem, SelectGroup } from "../ui/select"
import { useCategories } from "@/hooks/Categories/useCategories"
import type { Asset } from "@/types/assets/asset"

type props = {
    asset : Asset | null;
    onClose: () => void;
}
export default function UpdateAssetModal({asset, onClose}: props) {
    if (!asset) return null;
    const { data :categories } = useCategories();
    const selectedCategory = categories?.find(
        (c : any) => c.name === asset.categoryName
    )
    return (
        <div className="fixed inset-0 bg-black/70 flex justify-center items-center z-50">
            <div className="bg-slate-100 p-6 rounded w-full max-w-md">
                <div className="flex justify-between">
                    <h2 className="text-2xl">Edit Asset {asset.name}</h2>
                    <Button variant="ghost" size="sm" onClick={onClose}>&times;</Button>
                </div>
                <form action="">
                    <FieldGroup>
                        <Field>
                            <FieldLabel>Name</FieldLabel>
                            <Input type="text" placeholder="Asset Name" defaultValue={asset.name} />
                        </Field>
                        <Field>
                            <FieldLabel>Category</FieldLabel>
                            <Select defaultValue={selectedCategory ? String(selectedCategory.id) : undefined}>
                                <SelectTrigger>
                                    <SelectValue  />
                                </SelectTrigger>
                                <SelectContent>
                                    <SelectGroup>
                                        {categories && categories.map((category: any) => (
                                            <SelectItem key={category.id} value={String(category.id)}>
                                                {category.name}
                                            </SelectItem>
                                        ))}
                                    </SelectGroup>
                                </SelectContent>
                            </Select>
                        </Field>
                        <Field>
                            <FieldLabel>Purchase Price</FieldLabel>
                            <Input type="number" placeholder="Asset Name" defaultValue={asset.purchasePrice} />
                        </Field>
                    </FieldGroup>
                </form>
            </div>
        </div>
    )
}