import { INumberVariable } from "./INumberVariable";

export type VariableOptions = INumberVariable 


export interface IVariable {
    name: string;
    type: string;
    variableData?: VariableOptions;
}