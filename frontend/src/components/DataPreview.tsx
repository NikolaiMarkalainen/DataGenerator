import { PrimaryButton, DefaultButton, Stack, Text } from "@fluentui/react";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../store";
import { variableNameForNumber } from "../types/variableEnum";


export const DataPreview = () => {
    const variables = useSelector((state: RootState) => state.variables.variables);
    console.log(variables);
    const navigate = useNavigate();

    const navigateBack = () => {
        navigate('/');
    };

    const generateData = () => {
        console.log("generate data")
    }
    return (
        <Stack horizontal horizontalAlign="center" styles={{root: {minHeight: "90vh",}}}>
            <Stack tokens={{childrenGap: 16}} style={{width: "50%"}}>
                {variables && variables.length > 0 ? (
                    variables.map((m, index) => {
                        const typeToName = variableNameForNumber[m.type as keyof typeof variableNameForNumber];
                        return (
                            <Stack
                                key={index} 
                                enableScopedSelectors
                                horizontal
                                tokens={{ childrenGap: 16, padding: 22 }}
                                styles={{root: {justifyContent: "space-around", backgroundColor:'#FFAE42', borderBottom: "1px solid black"}}}
                            >
                                <Stack.Item styles={{root: {flexBasis: "40%", textAlign: 'center', backgroundColor:'#FFAE42'}}}>
                                    <Text style={{color: "grey"}}>"{m.name}"</Text>
                                </Stack.Item>
                                <Stack.Item styles={{root: {flexBasis: "40%", textAlign: 'center', backgroundColor:'#FFAE42'}}}>
                                    <Text>{typeToName}</Text>
                                </Stack.Item>
                            </Stack>
                        );
                    })
                ) : (
                    <Text>No variables available</Text>
                )}
                <Stack>
                    <PrimaryButton onClick={generateData}>Genereate Data</PrimaryButton>
                    <DefaultButton onClick={navigateBack}>Go Back</DefaultButton>
                </Stack>
            </Stack>
        </Stack>
    );

};