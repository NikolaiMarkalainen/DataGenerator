import { ICountryString } from "./ICountryString";
import { ICustomObject } from "./ICustomObject";
import { IFixedString } from "./IFixedString";
import { INumberVariable } from "./INumberVariable";
import { IOpenString } from "./IOpenString";

export type VariableOptions = INumberVariable | IFixedString | IOpenString | ICountryString | ICustomObject


export interface IVariable {
    name: string;
    type: number | string;
    variableData?: VariableOptions;
}