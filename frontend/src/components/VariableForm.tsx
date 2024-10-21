import React, { useEffect } from 'react';
import {NumberVariables} from './NumberVariables'
import { Stack } from '@fluentui/react';
import { NewVariableMenu } from './NewVariableMenu';
import { DefaultButton } from '@fluentui/react';
import { useState } from 'react';
import { IVariable } from '../types/IVariable';
import { PrimaryButton } from '@fluentui/react';
import { DataPreview } from './DataPreview';
import { useNavigate } from 'react-router-dom';
export const VariableForm = () => {


  const [variables, setVariables] = useState<IVariable[]>([]);
  const [variableSubmitBlock, setVariableSubmitBlock] = useState<boolean>(true);

  const navigate = useNavigate();
  useEffect(() => {
    // the data stays pure and we can submit to back end
    setVariableSubmitBlock(variables.every(variable => variable.variableData === undefined));

  }, [variables]);
  console.log(variableSubmitBlock);
  const addNewVariable = () => {
    setVariables([...variables, {name: "", type: ""}])
  };

  const handleVariableChange = (index: number, variableData: IVariable) => {
    const updatedVariables = [...variables];
    updatedVariables[index] = variableData;
    setVariables(updatedVariables);
  };

  const handleVariableDelete = (index: number) => {
    const updatedVariables = variables.filter((_, i) => i !== index);
    setVariables(updatedVariables);
  };

  const moveToPreview = () => {
    // head to new component where we display the json preview
    navigate('/preview');
  }
  
  //new variable
  return(
    <Stack tokens={{childrenGap: 16, padding: 12}}>
      {variables.map((variable,index) => (
        <NewVariableMenu
          key={index}
          index={index}
          variable={variable}
          onChange={handleVariableChange}
          onDelete={() => handleVariableDelete(index)}
        />
      ))}
        <PrimaryButton text="Continue" onClick={moveToPreview} disabled={variableSubmitBlock}/>

        <DefaultButton
          text="Add variable"
          onClick={addNewVariable}
        />

    </Stack>
  )
}


