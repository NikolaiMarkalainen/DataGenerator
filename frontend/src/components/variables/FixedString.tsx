import { Stack, TextField } from "@fluentui/react";
import { useEffect, useState } from "react";
import { IFixedString } from "../../types/IFixedString";
import { AcceptDecline } from "../AcceptDecline";

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

    

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {

        if(event.key === 'Enter'){
            props.onChange({fixedString});
        }
    };

    const handleSubmit = () => {
        props.onChange({fixedString});
    };
    return(
        <Stack>
            <TextField
                value={fixedString} 
                label="Set fixed string"
                description="This will appear in every instance of the data"
                onChange={(e, value) => setFixedString(value || "")}
                onKeyDown={handleKeyPress}
            />
            <AcceptDecline onChange={handleSubmit} onDelete={props.onDelete}/>
        </Stack>
    )
};