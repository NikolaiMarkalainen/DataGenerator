import { Stack, IDropdownOption, Dropdown } from "@fluentui/react";
import { useEffect, useState } from "react";
import { IIDObject, IdTypes } from "../../types/IIDObject";

type Props = {
    onChange: (variable: IIDObject) => void
};

export const RandomId = (props: Props) => {
    // create random id of format number or UUID 
    const [selectedItem, setSelectedItem] = useState<IDropdownOption>();
    const idMenuOptios = 
    [
        { key: IdTypes.NUMBER, text: "Chronological numbers"},
        { key: IdTypes.UUID, text: "UUID generated id"},
    ]

    const handleDropdownChange = (event: React.FormEvent<HTMLDivElement>, item?: IDropdownOption) => {
        setSelectedItem(item);
    }

    useEffect(() => {
        if(selectedItem) {
            props.onChange({idType: Number(selectedItem?.key)});
        }
    }, [selectedItem]);


    return(
        <Stack>
            <Dropdown
                placeholder="Select Id type"
                selectedKey={selectedItem?.key}
                onChange={handleDropdownChange}
                options={idMenuOptios}
            />
        </Stack>
    )
};