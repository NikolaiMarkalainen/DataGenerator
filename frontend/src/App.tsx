import React, { useEffect } from 'react';
import {NumberVariables} from './components/NumberVariables'
import { Stack } from '@fluentui/react';
import { NewVariableMenu } from './components/NewVariableMenu';
import { DefaultButton } from '@fluentui/react';
import { useState } from 'react';
import { IVariable } from './types/IVariable';
import { PrimaryButton } from '@fluentui/react';
export const App = () => {


  const [variables, setVariables] = useState<IVariable[]>([]);
  const [variableSubmitBlock, setVariableSubmitBlock] = useState<boolean>(true);

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

  const generateDataRequest = () => {
    // head to new component where we display the json preview
    console.log("nothing yet here POSTING");
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
        <PrimaryButton text="Continue" onClick={generateDataRequest} disabled={variableSubmitBlock}/>
        
        <DefaultButton
          text="Add variable"
          onClick={addNewVariable}
        />

    </Stack>
  )
}


