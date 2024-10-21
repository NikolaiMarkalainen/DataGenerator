import { PrimaryButton, Stack } from "@fluentui/react";
import { IVariable } from "../types/IVariable";
import { useNavigate } from "react-router-dom";




export const DataPreview = () => {
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