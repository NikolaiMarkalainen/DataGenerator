import { configureStore } from "@reduxjs/toolkit";
import { variableSlice } from './reducers/variableReducer';

const store = configureStore({
    reducer:{
        variables: variableSlice.reducer,
    }
});


export type RootState = ReturnType<typeof store.getState>;

export default store