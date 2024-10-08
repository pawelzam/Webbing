import { CustomerInfo } from "./customeInfo";

export interface Usage {
    lp?: number;    
    title?: string;
    subtitle?: string;
    count?: string;
    unit?: string;
    customerInfos?: CustomerInfo[];
}