import {z} from "zod";

export const updateAssetSchema = z.object({
 name: z.string().min(1, "Nama wajib diisi"),
  categoryId: z.number(),
  purchasePrice: z.number().min(0),
  locationId: z.number(),
  currentHolderId: z.number(),
  status: z.string(),
})

export type updateAssetFormValues = z.infer<typeof updateAssetSchema>