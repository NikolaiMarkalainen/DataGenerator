import { Dropdown, Stack, IDropdownOption, Toggle, TextField, FontIcon } from "@fluentui/react";
import { useEffect, useState } from "react";
import { ICountryString } from "../../types/ICountryString";
import { validateInputFormat } from "../helpers/validationHelper";

type Props = {
    onChange: (variableContents: ICountryString) => void;
    variableContent: ICountryString;
    onDelete: () => void;
}

export const CountrString = (props: Props) => {
    // set country to fixed or random full name or Short version

    const [selectedCountry, setSelelectedCountry] = useState<IDropdownOption>();
    const [countries, setCountries] = useState([]);
    const [randomCountry, setRandomCountry] = useState<boolean>(false);
    const [amountFixed, setAmountFixed] = useState<number>(1);

    useEffect(() => { 
        const fetchCountries = async () => {
            try{
                const response = await fetch('/json_data/countries.json');
                const data = await response.json();
                setCountries(data);
            }   catch(error) {
                console.log(error);
            }
        };
        fetchCountries();
    }, []);



    useEffect(() => {
        if(randomCountry){
            setSelelectedCountry({key: "", text: ""});
        }
    }, [randomCountry])

    useEffect(() => {
        if(props.variableContent){
            if(props.variableContent.fixed && props.variableContent.amountFixed){
                setAmountFixed(props.variableContent.amountFixed);
            }
            if(props.variableContent.key && props.variableContent.text){
                setSelelectedCountry({key: props.variableContent.key, text: props.variableContent.text})
            }
            if(props.variableContent.fixed){
                setRandomCountry(props.variableContent.fixed);
            }
        }
    }, [props.variableContent])

    useEffect(() => {
        props.onChange({
            text: selectedCountry?.text,
            key: selectedCountry?.key.toString(),
            fixed: randomCountry,
            amountFixed: amountFixed,
        });
    }, [randomCountry, amountFixed])


    const handleDropdownChange= (event: React.FormEvent<HTMLDivElement>, item?: IDropdownOption)=> {
        setSelelectedCountry(item);
        if(selectedCountry)
        {
            props.onChange({
                text: selectedCountry?.text,
                key: selectedCountry?.key.toString(),
                fixed: randomCountry,
                amountFixed: amountFixed,
            }); 
        }
    }

    const handleToggleChange = (e: React.MouseEvent<HTMLElement>, checked?: boolean) => {
        setRandomCountry(checked || false);
        if(randomCountry) {
            setAmountFixed(0);
        }
            
    }

    return(
        <Stack tokens={{padding: 12, childrenGap: 8}}>
            <Dropdown
                disabled={randomCountry}
                placeholder="Select a country"
                selectedKey={selectedCountry?.key}
                onChange={handleDropdownChange}
                options={countries}
            /> 
                <Stack tokens={{padding: 12}}>
                    <Toggle
                        onText="Random Countries"
                        offText="Singular Country"
                        checked={randomCountry}
                        onChange={handleToggleChange}
                    />
                    <TextField
                        disabled={!randomCountry}
                        label="Set amount of countries"
                        value={amountFixed.toString()}
                        description="Adjust the amount of random countries, Default will generate a random country for each entry"
                        onChange={(input, text) => validateInputFormat(text || '', setAmountFixed)}
                    />
                </Stack>
        </Stack>
    )
};