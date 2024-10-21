import { PrimaryButton, Stack } from "@fluentui/react";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../store";




export const DataPreview = () => {
    const variables = useSelector((state: RootState) => state.variables);
    console.log(variables);
    const navigate = useNavigate();

    const navigateBack = () => {
        navigate('/');
    };

    return(
        <Stack>
            asdasdas
            <PrimaryButton onClick={navigateBack}/>
        </Stack>
    )
};