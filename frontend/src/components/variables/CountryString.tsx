import { Dropdown, Stack, IDropdownOption, Toggle, TextField, FontIcon } from "@fluentui/react";
import { useEffect, useState } from "react";
import { ICountryString } from "../../types/ICountryString";
import { validateInputFormat } from "../helpers/validationHelper";


type Props = {
    onChange: (variableContents: ICountryString) => void;
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

    const handleDropdownChange= (event: React.FormEvent<HTMLDivElement>, item?: IDropdownOption)=> {
        setSelelectedCountry(item);
    }

    const handleToggleChange = (e: React.MouseEvent<HTMLElement>, checked?: boolean) => {
        setRandomCountry(checked || false);
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
                        onText="Fixed Country"
                        offText="Random Country"
                        checked={randomCountry}
                        onChange={handleToggleChange}
                    />
                    <TextField
                        disabled={!randomCountry}
                        label="Set amount of countries fixed"
                        description="Adjust the amount of random countries, Default will generate a random country for each entry"
                        onChange={(input, text) => validateInputFormat(text || '', setAmountFixed)}
                    />
                </Stack>
        </Stack>
    )
};