import { PayloadAction } from "@reduxjs/toolkit";
import { IVariable } from "../types/IVariable"
import { createSlice } from "@reduxjs/toolkit";

interface AppState {
    variables: IVariable[]
};

const initialState: AppState = {
    variables: []
};


export const variableSlice = createSlice({
    name: 'variables',
    initialState,
    reducers: {
        addMultipleVariables: (state, action: PayloadAction<IVariable[]>) => {
            state.variables = (action.payload);
        },
        addVariable: (state, action: PayloadAction<IVariable>) => {
            state.variables.push(action.payload);
        },
        updateVariable: (state, action: PayloadAction<{index: number; variable: IVariable}>) => {
            const {index, variable} = action.payload;
            state.variables[index] = variable;
        },
        deleteVariable: (state, action: PayloadAction<number>) => {
            state.variables.splice(action.payload, 1)
        },
    },
});

export const { addMultipleVariables, addVariable, updateVariable, deleteVariable } = variableSlice.actions;