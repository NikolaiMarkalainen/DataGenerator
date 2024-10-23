import React, { useEffect } from 'react';
import { Stack } from '@fluentui/react';
import { NewVariableMenu } from './NewVariableMenu';
import { DefaultButton } from '@fluentui/react';
import { useState } from 'react';
import { IVariable } from '../types/IVariable';
import { PrimaryButton } from '@fluentui/react';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { RootState } from '../store';
import { useSelector } from 'react-redux';
import { addVariable, deleteVariable, updateVariable } from '../reducers/variableReducer';

export const VariableForm = () => {


  const [variableSubmitBlock, setVariableSubmitBlock] = useState<boolean>(true);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const variables = useSelector((s: RootState) => s.variables.variables);

  useEffect(() => {
    // the data stays pure and we can submit to back end
    setVariableSubmitBlock(variables.every(variable => variable.variableData === undefined));

  }, [variables]);

  
  const addNewVariable = () => {
    const newVariable = { name: "", type: ""};
    // new redux variable
    dispatch(addVariable(newVariable))
  };

  const handleVariableChange = (index: number, variableData: IVariable) => {
    // updating to redux store
    dispatch(updateVariable({index, variable: variableData}));
  };

  const handleVariableDelete = (index: number) => {
    //delete by id
    dispatch(deleteVariable(index));
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
        <PrimaryButton
          text="Add new"
          onClick={addNewVariable}
        />
        <DefaultButton text="Continue" style={{backgroundColor:'#FFAE42'}} onClick={moveToPreview} disabled={variableSubmitBlock}/>

    </Stack>
  )
}


