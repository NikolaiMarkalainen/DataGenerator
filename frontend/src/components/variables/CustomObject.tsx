import { Stack, DefaultButton } from "@fluentui/react";
import { NewVariableMenu } from "../NewVariableMenu";
import { ICustomObject } from "../../types/ICustomObject";
import { IVariable } from "../../types/IVariable";
import { useState } from "react";

type Props = {
    onChange: (variableData: ICustomObject) => void;
};


export const CustomObject = (props: Props) => {
    const [fields, setFields] = useState<(IVariable)[]>([]);
    // import custom object or create own custom object
    // TODO NESTED OBJECTS
    const addNewVariable = () => {
        const newVariable: IVariable = {name: "", type: ""};
        const updatedFields = [...fields, newVariable];
        setFields(updatedFields);
        props.onChange({fields: updatedFields});
    };

    const handleVariableChange = (index: number, variableData: IVariable) => {
        const updatedFields = [...fields];
        updatedFields[index] = variableData;
        setFields(updatedFields);
        props.onChange({fields: updatedFields});
    };

    const handleVariableDelete = (index: number) => {
        const updateFields = fields.filter((_, i) => i !== index);
        setFields(updateFields);
        props.onChange({fields: updateFields})
    };

    return(
        <Stack tokens={{childrenGap: 16, padding: 12}}>
            {fields.map((variable,index) => (
            <NewVariableMenu
                key={index}
                index={index}
                variable={variable}
                onChange={handleVariableChange}
                onDelete={() => handleVariableDelete(index)}
            />
            ))}
            <DefaultButton
            text="Add new"
            onClick={addNewVariable}
            />
        </Stack>
    )
};