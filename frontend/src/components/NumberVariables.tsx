import React, { useEffect } from 'react';
import { Slider, Stack, TextField, Toggle, Dropdown , IDropdownOption, DropdownMenuItemType} from '@fluentui/react';
import { useState } from 'react';

enum SliderFormats {
  Integer = 0,
  Float = 1,
}

export const NumberVariables = () => {

  //slider state
  
  const [sliderValue, setSliderValue] = useState<number>();
  const [sliderRange, setSliderRange] = useState<[number, number]>([0, 10]);
  const [sliderMin, setSliderMin] = useState<number>(0);
  const [sliderMax, setSliderMax] = useState<number>(0);
  const [sliderStep, setSliderStep] =useState<number>(1);
  const [decimalPrecision, setDecimalPrecision] = useState<number>(0);
  const [decimalError, setDecimalError] = useState<boolean>(false);
  const [selectedItem, setSelectedItem] = useState<IDropdownOption>();

  const dropDownOptions = [
      {key: 0, text: 'Primary numbers'},
      {key: 1, text: 'Decimal numbers'},
      {key: 2, text: 'Percentage'}
  ];

  const sliderValueFormat = (value: number): string =>{
    if(selectedItem?.key === SliderFormats.Float) {
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
      if(/^\d*$/.test(input) && numberInput <= 10){
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


  const handleDropdownChange = (event: React.FormEvent<HTMLDivElement>, item?: IDropdownOption): void => {
    setSelectedItem(item);
    console.log(selectedItem)
  };


  return (
    <Stack horizontal style={{backgroundColor:'red'}}>
      <Stack grow={1} style={{backgroundColor: 'green'}}>
        <Stack.Item>
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
          {selectedItem?.key === SliderFormats.Float && (
          <TextField
            label='Decimal precision'
            maxLength={2}
            errorMessage={decimalError ? "Decimal precision is at max 10" : ""}
            value={decimalPrecision.toString() || '0'}
            onChange={(input, text) => validateInputFormat(text || '', setDecimalPrecision, true)}
          />
          )}
        </Stack.Item>
      </Stack>
      <Stack grow={5} style={{ backgroundColor: 'yellow'}}>
        <Stack.Item>
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
          <Dropdown
          placeholder="Select number format"
          selectedKey={selectedItem ? selectedItem.key : undefined}
          onChange={handleDropdownChange}
          options={dropDownOptions}
          />
        </Stack.Item>
      </Stack>
    </Stack>

  );
};