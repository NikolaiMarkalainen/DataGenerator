import { Stack, TextField } from "@fluentui/react";
import { useState } from "react";
import { IFixedString } from "../../types/IFixedString";

type Props = {
    onChange: (variableContents: IFixedString) => void;
}



export const FixedString = (props: Props) => {
    // set string that will appear consistently in the data sample

    const [fixedString, setFixedString] = useState<string>('');


    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {

        if(event.key === 'Enter'){
            props.onChange({fixedString});
        }
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
        </Stack>
    )
};