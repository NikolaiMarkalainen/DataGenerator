import { Dropdown, FontIcon, IDropdownOption, mergeStyles, PrimaryButton, Stack, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { variableMenuItems } from "../types/variableEnum";
import { NumberVariables } from "./NumberVariables";
import { IVariable, VariableOptions } from "../types/IVariable";


type Props = {
    index: number;
    variable: IVariable;
    onChange: (index: number, variableData: IVariable) => void;
    onDelete: () => void;
}

const iconClass = mergeStyles({
    fontSize: 24,
    height: 24,
    padding: 8,
    width: 24,
    alignContent: 'end'
})

export const NewVariableMenu = (props: Props) => {

    const [selectedItem, setSelectedItem] = useState<IDropdownOption>();
    const [variableName, setVariableName] = useState<string>();
    const [variableContent, setVariableContent] = useState<VariableOptions>();

    const variablesMenuOptions = [
        {key: variableMenuItems.NUMBER, text: "Number"},
        {key: variableMenuItems.OPEN_STRING, text: "Open String"},
        {key: variableMenuItems.FIXED_STRING, text: "Fixed String"},
        {key: variableMenuItems.RANDOM_FIRST_NAME, text: "Random first name string"},
        {key: variableMenuItems.RANDOM_LAST_NAME, text: "Random last name string"},
        {key: variableMenuItems.RANDOM_CITY, text: "Random City"},
        {key: variableMenuItems.RANDOM_CUSTOM_OBJECT, text: "Custom Object"},
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

    const handleNumberVariableChange = (numberVariables: VariableOptions) => {
        setVariableContent(numberVariables);
    };

    useEffect(() => {
        handleSubmit();
    }, [variableContent])

    return(
        <Stack tokens={{childrenGap: 16}}>
                <TextField
                    label="Write variable name"
                    value={variableName ?? props.variable.name}
                    onChange={(e, value) => setVariableName(value || "")}
                />
                <Dropdown
                    placeholder="Select a variable"
                    selectedKey={selectedItem  ? selectedItem.key : props.variable.type}
                    onChange={handleDropdownChange}
                    options={variablesMenuOptions}
                />
            {(selectedItem?.key === variableMenuItems.NUMBER) && (
                <NumberVariables onChange={handleNumberVariableChange}/>
            )}
            <Stack.Item align="end">
                <FontIcon iconName="Accept" className={iconClass} onClick={handleSubmit}/>
                <FontIcon iconName="Delete" className={iconClass} onClick={props.onDelete}/>
            </Stack.Item>

        </Stack>
    )
};