import React, { useEffect } from 'react';
import { Slider, Stack, TextField, Toggle, Dropdown , IDropdownOption, DropdownMenuItemType, DefaultButton} from '@fluentui/react';
import { useState } from 'react';
import { INumberVariable } from '../../types/INumberVariable';
import { validateInputFormat } from '../helpers/validationHelper';
import { AcceptDecline } from '../AcceptDecline';

type Props = {
  onChange: (variableContents: INumberVariable) => void;
  variableContent: INumberVariable;
  onDelete: () => void;
}


export const NumberVariables = (props: Props) => {
  
  const [sliderValue, setSliderValue] = useState<number>();
  const [sliderMin, setSliderMin] = useState<number>(0);
  const [sliderMax, setSliderMax] = useState<number>(10);
  const [sliderStep, setSliderStep] =useState<number>(1);
  const [decimalPrecision, setDecimalPrecision] = useState<number>(0);
  const [decimalError, setDecimalError] = useState<boolean>(false);
  const [isDecimal, setIsDecimal] = useState<boolean>(false);

  useEffect(() => {
    if(props.variableContent){
        if(props.variableContent.decimal && props.variableContent.decimalPrecision){
          setDecimalPrecision(props.variableContent.decimalPrecision)
        }
        if(props.variableContent.decimal){
          setIsDecimal(props.variableContent.decimal);
        }
        if(props.variableContent.max){
          setSliderMax(props.variableContent.max);
        }
        if(props.variableContent.min){
          setSliderMin(props.variableContent.min);
        }
    }
  }, [props.variableContent]);


 
  const sliderValueFormat = (value: number): string =>{
    if(isDecimal) {
      const scaledValue = value / Math.pow(10, decimalPrecision);
      return scaledValue.toFixed(decimalPrecision);
    } 
    else {
      return value.toString();
    }
  };


  const onSliderChange = (value: number, range?: [number, number]) => {
    setSliderValue(value);
  };

  const handleToggleChange = (e:React.MouseEvent<HTMLElement>, checked?: boolean ) => {
    setIsDecimal(checked || false);
  }

  const handlePropertySubmit = () => {
    props.onChange({ 
      min: sliderMin, 
      max: sliderMax, 
      decimalPrecision: decimalPrecision, 
      decimal: isDecimal})
  }

  return (
    <Stack>
      <Stack horizontal>
        <Stack tokens={{ padding: 32, childrenGap: 8 }} grow={1}>
            <TextField
                label='Enter integer minimum value'
                value={sliderMin.toString()}
                onChange={(input, text) => validateInputFormat(text || '', setSliderMin)}
              />
            <TextField
              label='Enter integer maximum value'
              value={sliderMax.toString()}
              onChange={(input, text) => validateInputFormat(text || '', setSliderMax)}
            />
            {isDecimal && (
            <TextField
              label='Decimal precision'
              maxLength={2}
              errorMessage={decimalError ? "Decimal precision is at max 4" : ""}
              value={decimalPrecision.toString() || '0'}
              onChange={(input, text) => validateInputFormat(text || '', setDecimalPrecision, true, setDecimalError)}
            />
            )}
        </Stack>
        <Stack tokens={{ padding: 18}} grow={7}>
            <Toggle
              label="Set Decimal"
              onText='Decimal'
              offText='Primary'
              checked={isDecimal}
              onChange={handleToggleChange}
            />
            <Slider 
              label="Integer range" 
              styles={{root: {paddingTop: 16}}}
              ranged
              value={sliderValue} 
              min={sliderMin}
              max={sliderMax} 
              showValue
              step={sliderStep}
              onChanged={(e, value, range) => (onSliderChange(value, range))}
              valueFormat={sliderValueFormat}
            />
        </Stack>
      </Stack>
      <AcceptDecline onChange={handlePropertySubmit} onDelete={props.onDelete}/>
    </Stack>
  );
};