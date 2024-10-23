import { FontIcon, mergeStyles, Stack } from "@fluentui/react"
import { VariableOptions } from "../types/IVariable";
import { IUseProp } from "../types/IUseProp";

type Props = {
    onChange?: () => void;
    onDelete: () => void;
    onVariableAccept?: (accept: IUseProp) => void;
}


const iconClass = mergeStyles({
    fontSize: 24,
    height: 24,
    padding: 8,
    width: 24,
    alignContent: 'end'
});


export const AcceptDecline = (props: Props) => {

    const acceptUserSetting = () => {
        if(props.onVariableAccept) {
            props.onVariableAccept({useProperty: true});
        }
    };


    return(
        <Stack tokens={{padding: 16}} horizontal styles={{ root: { justifyContent: 'space-between' }}}>
            <Stack.Item align="start">
                {props.onChange || props.onVariableAccept ? (
                <FontIcon iconName="Accept" className={iconClass} onClick={props.onChange ?? acceptUserSetting}/>
                ) : <></>}
            </Stack.Item>
            <Stack.Item align="end">
                <FontIcon iconName="Delete" className={iconClass} onClick={props.onDelete} 
                    style={!props.onChange || !props.onVariableAccept ? { alignSelf: 'end'}: {} }
                />
            </Stack.Item>
        </Stack>
    )
}