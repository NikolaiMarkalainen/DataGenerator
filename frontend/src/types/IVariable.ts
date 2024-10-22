import { ICountryString } from "./ICountryString";
import { ICustomObject } from "./ICustomObject";
import { IFixedString } from "./IFixedString";
import { INumberVariable } from "./INumberVariable";
import { IOpenString } from "./IOpenString";
import { IIDObject } from "./IIDObject";
import { IUseProp } from "./IUseProp";

export type VariableOptions = INumberVariable | IFixedString | IOpenString | ICountryString | ICustomObject | IIDObject | IUseProp


export interface IVariable {
    name: string;
    type: number | string;
    variableData?: VariableOptions;
}