import React from "react"
import { BrowserRouter, Routes, Route } from "react-router-dom"
import { VariableForm } from "./components/VariableForm"
import { DataPreview } from "./components/DataPreview"


export const App = () => {


  return(
    <>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<VariableForm />}/>
        <Route path="/preview/" element={<DataPreview />}/>
      </Routes>
    </BrowserRouter>
    </>
  
  )
}


