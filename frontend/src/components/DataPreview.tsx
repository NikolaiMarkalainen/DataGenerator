import { PrimaryButton, DefaultButton, Stack, Text, TextField, Toggle } from "@fluentui/react";
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
    const [json, setJson] = useState<boolean>(true);

    console.log(variables);
    const navigate = useNavigate();
    
    const navigateBack = () => {
        navigate('/');
    };

    useEffect(() => {
        if(Number(amount)){
            setRequestBody({"variables": variables, "amount" : amount, "jsonFile": json });
        }
    },[amount, json]);

    const handleToggleChange = (e: React.MouseEvent<HTMLElement>, check?: boolean) => {
        setJson(check || false);
    };

    const generateData = async () => {
        if (amount !== 0) {
            try {
                const fileType = json ? 'json' : 'csv';
                const responseType = 'blob';
    
                const fileExtension = fileType === 'json' ? 'json' : 'csv';
                const contentType = fileType === 'json' ? 'application/json' : 'text/csv';
    
                const response = await axios.post("http://localhost:5300/generate/file", requestBody, {
                    responseType: responseType,
                    headers: {
                        'Accept': contentType, 
                    },
                });
    
                const blob = new Blob([response.data], { type: contentType });
                
                const link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = `GeneratedData_${new Date().toISOString().slice(0, 19).replace(/T/g, '_')}.${fileExtension}`; // Adjust file name with the correct extension
                
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
                window.URL.revokeObjectURL(link.href);
    
                console.log(response.data);
            } catch (error) {
                console.error(error);
            }
        }
    };
    

    return (
        <Stack horizontal horizontalAlign="center" styles={{root: {minHeight: "90vh", marginTop: 24}}}>
            <Stack tokens={{childrenGap: 16}} style={{width: "50%", border: "1px solid black"}}>
                {variables && variables.length > 0 ? (
                    variables.map((m, index) => {
                        const typeToName = variableNameForNumber[m.type as keyof typeof variableNameForNumber];
                        return (
                            <Stack
                                key={index} 
                                enableScopedSelectors
                                horizontal
                                tokens={{ childrenGap: 16, padding: 22 }}
                                styles={{root: {justifyContent: "space-around", border: "1px solid black"}}}
                            >
                                <Stack.Item styles={{root: {flexBasis: "40%", textAlign: 'center'}}}>
                                    <Text>"{m.name}"</Text>
                                </Stack.Item>
                                <Stack.Item styles={{root: {flexBasis: "40%", textAlign: 'center'}}}>
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
                    <Toggle
                    label={ json ? <span><b>JSON</b> file will be generated</span> :
                            <span><b>CSV</b> file will be generated, Ignoring <b>CUSTOM_OBJECT</b> field</span>
                    }                    
                    checked={json}
                    onChange={handleToggleChange}
                    />

                    <PrimaryButton style={{marginBottom: 16}} onClick={generateData}>Genereate Data</PrimaryButton>
                    <DefaultButton onClick={navigateBack}>Go Back</DefaultButton>
                </Stack>
            </Stack>
        </Stack>
    );

};