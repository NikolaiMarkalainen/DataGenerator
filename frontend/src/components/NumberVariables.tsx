import React, { useEffect } from 'react';
import { Slider, Stack, TextField, Toggle, Dropdown , IDropdownOption, DropdownMenuItemType} from '@fluentui/react';
import { useState } from 'react';

enum SliderFormats {
  Integer = 0,
  Float = 1,
}

export const NumberVariables = () => {
  
  const [sliderValue, setSliderValue] = useState<number>();
  const [sliderRange, setSliderRange] = useState<[number, number]>([0, 10]);
  const [sliderMin, setSliderMin] = useState<number>(0);
  const [sliderMax, setSliderMax] = useState<number>(10);
  const [sliderStep, setSliderStep] =useState<number>(1);
  const [decimalPrecision, setDecimalPrecision] = useState<number>(0);
  const [decimalError, setDecimalError] = useState<boolean>(false);
  const [selectedItem, setSelectedItem] = useState<IDropdownOption>();

  const [isDecimal, setIsDecimal] = useState<boolean>(false);

  const dropDownOptions = [
      {key: 0, text: 'Primary numbers'},
      {key: 1, text: 'Decimal numbers'},
  ];

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

  const validateInputFormat = (input: string, setState: React.Dispatch<React.SetStateAction<number>>, precision?: boolean) => {
    if(input === "") {
      setState(0);
      return;
    }
    const numberInput = Number(input);
    if(precision){
      if(/^\d*$/.test(input) && numberInput <= 4){
          setState(numberInput);
          setDecimalError(false);
      } else {
        setDecimalError(true);
      }
    } 
    else {
      if(/^\d*$/.test(input)) {
        setState(numberInput)
      }
    }
  };





  const handleToggleChange = (e:React.MouseEvent<HTMLElement>, checked?: boolean ) => {
    setIsDecimal(checked || false);
  }
  return (
    <Stack horizontal>
      <Stack tokens={{ padding: 12, childrenGap: 8 }} grow={1}>
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
            onChange={(input, text) => validateInputFormat(text || '', setDecimalPrecision, true)}
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

  );
};