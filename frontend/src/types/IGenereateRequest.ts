import { IVariable } from "./IVariable";


export interface IGenerateRequest {
    variables: IVariable[];
    amount: number;
    jsonFile: boolean;
} 