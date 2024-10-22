import { ICountryString } from "./ICountryString";
import { ICustomObject } from "./ICustomObject";
import { IFixedString } from "./IFixedString";
import { INumberVariable } from "./INumberVariable";
import { IOpenString } from "./IOpenString";
import { IIDObject } from "./IIDObject";

export type VariableOptions = INumberVariable | IFixedString | IOpenString | ICountryString | ICustomObject | IIDObject


export interface IVariable {
    name: string;
    type: number | string;
    variableData?: VariableOptions;
}