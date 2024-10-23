import { Stack, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { IFixedString } from "../../types/IFixedString";

type Props = {
    onChange: (variableContents: IFixedString) => void;
    variableContent: IFixedString;
    onDelete: () => void;
}



export const FixedString = (props: Props) => {
    // set string that will appear consistently in the data sample

    const [fixedString, setFixedString] = useState<string>('');

    useEffect(() => {
        if(props.variableContent && props.variableContent.fixedString){
            setFixedString(props.variableContent.fixedString);
        }
    }, [props.variableContent]);

    
    useEffect(() => {
        props.onChange({fixedString});
    }, [fixedString])


    return(
        <Stack>
            <TextField
                value={fixedString} 
                label="Set fixed string"
                description="This will appear in every instance of the data"
                onChange={(e, value) => setFixedString(value || "")}
            />
        </Stack>
    )
};