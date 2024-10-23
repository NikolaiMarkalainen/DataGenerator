import { Dropdown, FontIcon, IDropdownOption, mergeStyles, PrimaryButton, Stack, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { variableMenuItems } from "../types/variableEnum";
import { NumberVariables } from "./variables/NumberVariables";
import { IVariable, VariableOptions } from "../types/IVariable";
import { OpenString } from "./variables/OpenString";
import { FixedString } from "./variables/FixedString";
import { CustomObject } from "./variables/CustomObject";
import { RandomId } from "./variables/RandomId";
import { CountrString } from "./variables/CountryString";
import { IFixedString } from "../types/IFixedString";
import { IOpenString } from "../types/IOpenString";
import { INumberVariable } from "../types/INumberVariable";
import { ICountryString } from "../types/ICountryString";
import { ICustomObject } from "../types/ICustomObject";
import { IIDObject } from "../types/IIDObject";
import { AcceptDecline } from "./AcceptDecline";


type Props = {
    index: number;
    variable: IVariable;
    onChange: (index: number, variableData: IVariable) => void;
    onDelete: () => void;
};

const iconClass = mergeStyles({
    fontSize: 24,
    height: 24,
    padding: 8,
    width: 24,
    alignContent: 'end'
});

export const NewVariableMenu = (props: Props) => {

    const [selectedItem, setSelectedItem] = useState<IDropdownOption>();
    const [variableName, setVariableName] = useState<string>();
    const [variableContent, setVariableContent] = useState<VariableOptions>();

    const variablesMenuOptions = [
        {key: variableMenuItems.NUMBER, text: "Number"},
        {key: variableMenuItems.OPEN_STRING, text: "Open String"},
        {key: variableMenuItems.FIXED_STRING, text: "Fixed String"},
        {key: variableMenuItems.RANDOM_FIRST_NAME, text: "Random First Name"},
        {key: variableMenuItems.RANDOM_LAST_NAME, text: "Random Last Name"},
        {key: variableMenuItems.RANDOM_COUNTRY, text: "Random Country"},
        {key: variableMenuItems.RANDOM_CUSTOM_OBJECT, text: "Custom Object"},
        {key: variableMenuItems.RANDOM_ID, text: "Unique ID"},
    ];

    const handleDropdownChange = (event: React.FormEvent<HTMLDivElement>, item?: IDropdownOption) => {
        setSelectedItem(item);
    };

    const handleSubmit = () => {
        if(variableName && selectedItem) {
            // TODO: add error handling for instances where stuff doesnt work
            props.onChange(props.index, {name: variableName, type: selectedItem.key, variableData: variableContent}, );
        }
    };

    const handleVariableChange = (content: VariableOptions) => {
        console.log("Content sent", content);
        setVariableContent(content);
    };



    const renderVariableComponent = () => {
        switch(selectedItem?.key) {
            case variableMenuItems.NUMBER:
                return <NumberVariables 
                            variableContent={props.variable.variableData as INumberVariable} 
                            onChange={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.OPEN_STRING:
                return <OpenString 
                            variableContent={props.variable.variableData as IOpenString} 
                            onChange={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.FIXED_STRING:
                return <FixedString 
                            variableContent={props.variable.variableData as IFixedString} 
                            onChange={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.RANDOM_FIRST_NAME:
                return <AcceptDecline
                            onVariableAccept={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.RANDOM_LAST_NAME:
                return <AcceptDecline
                            onVariableAccept={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.RANDOM_COUNTRY:
                return <CountrString 
                            variableContent={props.variable.variableData as ICountryString} 
                            onChange={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.RANDOM_CUSTOM_OBJECT:
                return <CustomObject 
                            variableContent={props.variable.variableData as ICustomObject} 
                            onChange={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            case variableMenuItems.RANDOM_ID:
                return <RandomId 
                            variableContent={props.variable.variableData as IIDObject} 
                            onChange={handleVariableChange}
                            onDelete={props.onDelete}
                            />
            default:
                return <AcceptDecline
                            onDelete={props.onDelete}
                            />
        };
    };

    useEffect(() => {
        handleSubmit();
    }, [variableContent])


    useEffect(() => {
        setSelectedItem(variablesMenuOptions.find(option => option.key === props.variable.type));
        setVariableName(props.variable.name);
        setVariableContent(props.variable.variableData);
    }, [props.variable]);



    return(
        <Stack tokens={{childrenGap: 16}}>
                <TextField
                    label="Write variable name"
                    value={variableName ?? props.variable.name}
                    description="This will be the key of the variable in every instance"
                    onChange={(e, value) => setVariableName(value || "")}
                />
                <Dropdown
                    placeholder="Select a variable"
                    selectedKey={selectedItem  ? selectedItem.key : props.variable.type}
                    onChange={handleDropdownChange}
                    options={variablesMenuOptions}
                />
            {renderVariableComponent()}
        </Stack>
    )
};