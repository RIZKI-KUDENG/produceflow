export const ASSET_STATUS ={
    IN_USE : "In Use",
    AVAILABLE : "Available",
    MAINTENANCE : "Maintenance",
    DISPOSSED : "Disposed"
} as const 

export type AssetStatus = (typeof ASSET_STATUS) [keyof typeof ASSET_STATUS];

export const ASSET_STATUS_OPTIONS: {
  value: AssetStatus;
  label: string;
}[] = [
  { value: ASSET_STATUS.IN_USE, label: "In Use" },
  { value: ASSET_STATUS.AVAILABLE, label: "Available" },
  { value: ASSET_STATUS.MAINTENANCE, label: "Maintenance" },
  { value: ASSET_STATUS.DISPOSSED, label: "Disposed" },
];