import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { FieldGroup, FieldLabel, Field } from "../ui/field"

type props = {
    assetId : number;
    onClose: () => void;
}
export default function UpdateAssetModal({assetId, onClose}: props) {
    if (!assetId) return null;
    return (
        <div className="fixed inset-0 bg-black/70 flex justify-center items-center z-50">
            <div className="p-6 rounded w-full max-w-md">
                <div className="flex justify-between">
                    <h2 className="text-2xl">Edit Asset {assetId}</h2>
                    <Button variant="ghost" size="sm" onClick={onClose}>&times;</Button>
                </div>
                <form action="">
                    <FieldGroup>
                        <Field>
                            <FieldLabel>Name</FieldLabel>
                            <Input type="text" placeholder="Asset Name" />
                        </Field>
                        <Field>
                            <FieldLabel>Category</FieldLabel>
                            <Input type="text" placeholder="Category" />
                        </Field>
                    </FieldGroup>
                </form>
            </div>
        </div>
    )
}