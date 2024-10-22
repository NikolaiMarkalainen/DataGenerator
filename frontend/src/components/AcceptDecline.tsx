import { FontIcon, mergeStyles, Stack } from "@fluentui/react"
import { VariableOptions } from "../types/IVariable";

type Props = {
    onChange: () => void;
    onDelete: () => void;
}


const iconClass = mergeStyles({
    fontSize: 24,
    height: 24,
    padding: 8,
    width: 24,
    alignContent: 'end'
});


export const AcceptDecline = (props: Props) => {


    return(
        <Stack horizontal>
            <FontIcon iconName="Delete" className={iconClass} onClick={props.onDelete}/>
            <FontIcon iconName="accept" className={iconClass} onClick={props.onChange}/>
        </Stack>
    )
}