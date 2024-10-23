import { Stack, DefaultButton, PrimaryButton } from "@fluentui/react";
import { NewVariableMenu } from "../NewVariableMenu";
import { ICustomObject } from "../../types/ICustomObject";
import { IVariable } from "../../types/IVariable";
import { useEffect, useState } from "react";
import { AcceptDecline } from "../AcceptDecline";

type Props = {
    onChange: (variableData: ICustomObject) => void;
    variableContent: ICustomObject;
    onDelete: () => void;
};


export const CustomObject = (props: Props) => {
    const [fields, setFields] = useState<(IVariable)[]>([]);
    // import custom object or create own custom object

    useEffect(() => {
        if(props.variableContent && props.variableContent.fields){
            setFields(props.variableContent.fields);
        }
    },[props.variableContent]);

    
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
            style={{backgroundColor:'#FFAE42'}}
            text="Add Object Child"
            onClick={addNewVariable}
            />
        <AcceptDecline onDelete={props.onDelete}/>
        </Stack>
    )
};