import { PrimaryButton, DefaultButton, Stack, Text, TextField } from "@fluentui/react";
import { useNavigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../store";
import { variableNameForNumber } from "../types/variableEnum";
import axios from "axios";
import { useEffect, useState } from "react";
import { IGenerateRequest } from "../types/IGenereateRequest";

export const DataPreview = () => {
    const variables = useSelector((state: RootState) => state.variables.variables);
    const [amount, setAmount] = useState<number>(0);
    const [requestBody, setRequestBody] = useState<IGenerateRequest>();
    console.log(variables);
    const navigate = useNavigate();
    
    const navigateBack = () => {
        navigate('/');
    };

    useEffect(() => {
        if(Number(amount)){
            setRequestBody({"variables": variables, "amount" : amount });
        }
    },[amount])
    
    const generateData = async () => {
        console.log("generate data");
        if(amount !== 0)
        {
            try{
                const response = await axios.post("http://localhost:5300/generate/file", requestBody, {
                    responseType: 'blob'
                });
                const blob = new Blob([response.data], { type: 'application/json' });
                const link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = `GeneratedData_${new Date().toISOString().slice(0, 19).replace(/T/g, '_')}.json`; 
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
                window.URL.revokeObjectURL(link.href);
                console.log(response.data);
            } catch(error){
                console.log(error);
            }
        }
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
                    <TextField 
                        label="Amount to generate" 
                        value={amount?.toString()}
                        onChange={(input, text) => setAmount(Number(text))}
                        errorMessage={"Only numerical values"}
                    />
                    <PrimaryButton style={{marginBottom: 16}} onClick={generateData}>Genereate Data</PrimaryButton>
                    <DefaultButton onClick={navigateBack}>Go Back</DefaultButton>
                </Stack>
            </Stack>
        </Stack>
    );

};