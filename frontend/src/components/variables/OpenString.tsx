import { Stack, TextField, Toggle } from "@fluentui/react";
import { IOpenString } from "../../types/IOpenString";
import { useEffect, useState } from "react";
import { validateInputFormat } from "../helpers/validationHelper";

type Props = {
    onChange: (variableContents: IOpenString) => void;
    variableContent: IOpenString;
    onDelete: () => void;
}

export const OpenString  = (props: Props) => {

    const [characterLength, setCharacterLength] = useState<number>(0);
    const [useWords, setUseWords] = useState<boolean>(false);

    useEffect(() => {
        if(props.variableContent){
            if(props.variableContent.words){
                setUseWords(props.variableContent.words);
            }
            if(props.variableContent.characterLength){
                setCharacterLength(props.variableContent.characterLength);
            }
        }
    }, [props.variableContent])


    const handleToggleChange = (e:React.MouseEvent<HTMLElement>, checked?: boolean) => {
        setUseWords(checked || false);
        if(!useWords) {
            setCharacterLength(0);
        };
    };

    useEffect(() => {
        props.onChange({
            characterLength: characterLength,
            words: useWords
        });
    }, [useWords, characterLength]);


    return(
        <Stack>
            <Stack tokens={{padding: 12, childrenGap: 8}} horizontalAlign="start">
                <TextField
                    value={characterLength?.toString()}
                    onChange={(input, text) => validateInputFormat(text || '', setCharacterLength )}
                    disabled={useWords}
                    description="Will limit random string length"
                    label="Set length for Random Characters"
                    styles={{root: {flexGrow: 1}}}
                />
                <Toggle
                onText="Real words"
                offText="Random Characters"
                checked={useWords}
                onChange={handleToggleChange}
                />
            </Stack>
        </Stack>
    )
};