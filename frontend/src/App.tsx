import React from 'react';
import {NumberVariables} from './components/NumberVariables'
import { Stack } from '@fluentui/react';
import { NewVariableMenu } from './components/NewVariableMenu';
import { DefaultButton } from '@fluentui/react';
import { useState } from 'react';
import { IVariable } from './types/IVariable';

export const App = () => {


  const [variables, setVariables] = useState<IVariable[]>([]);

  const addNewVariable = () => {
    setVariables([...variables, {name: "", type: ""}])
  }

  const handleVariableChange = (index: number, variableData: IVariable) => {
    const updatedVariables = [...variables];
    updatedVariables[index] = variableData;
    setVariables(updatedVariables);
  }

  const handleVariableDelete = (index: number) => {
    const updatedVariables = variables.filter((_, i) => i !== index);
    setVariables(updatedVariables);
  }
  console.log("VARIABLES", variables);
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
        <DefaultButton
          text="Add variable"
          onClick={addNewVariable}
        />
    </Stack>
  )
}


