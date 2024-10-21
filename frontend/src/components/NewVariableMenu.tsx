import { Dropdown, FontIcon, IDropdownOption, mergeStyles, PrimaryButton, Stack, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { variableMenuItems } from "../types/variableEnum";
import { NumberVariables } from "./NumberVariables";
import { IVariable, VariableOptions } from "../types/IVariable";
import { INumberVariable } from "../types/INumberVariable";

type Props = {
    index: number;
    variable: IVariable;
    onChange: (index: number, variableData: IVariable) => void;
    onDelete: () => void;
}

const iconClass = mergeStyles({
    fontSize: 24,
    height: 24,
    width: 24,
    alignContent: 'end'
})

export const NewVariableMenu = (props: Props) => {

    const [selectedItem, setSelectedItem] = useState<IDropdownOption>();
    const [variableName, setVariableName] = useState<string>('');
    const [variableContent, setVariableContent] = useState<VariableOptions>();

    const variablesMenuOptions = [
        {key: 0, text: "Number"},
        {key: 1, text: "Open String"},
        {key: 2, text: "Fixed String"},
        {key: 3, text: "Random first name string"},
        {key: 4, text: "Random last name string"},
        {key: 5, text: "Random City"},
        {key: 6, text: "Custom Object"},
    ];

    const handleDropdownChange = (event: React.FormEvent<HTMLDivElement>, item?: IDropdownOption) => {
        setSelectedItem(item);
    };

    const handleSubmit = () => {
        if(variableName && selectedItem) {
            // TODO: add error handling for instances where stuff doesnt work
            props.onChange(props.index, {name: variableName, type: selectedItem.text, variableData: variableContent}, );
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
                    value={variableName}
                    onChange={(e, value) => setVariableName(value || "")}
                />
                <Dropdown
                    placeholder="Select a variable"
                    selectedKey={selectedItem ? selectedItem.key : undefined}
                    onChange={handleDropdownChange}
                    options={variablesMenuOptions}
                />
            {selectedItem?.key === variableMenuItems.NUMBER && (
                <NumberVariables onChange={handleNumberVariableChange}/>
            )}
            <Stack.Item align="end">
                <FontIcon iconName="Delete" className={iconClass} onClick={props.onDelete}/>
            </Stack.Item>

        </Stack>
    )
};