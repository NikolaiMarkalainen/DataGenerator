

export const validateInputFormat = (
    input: string, 
    setState: React.Dispatch<React.SetStateAction<number>>, 
    precision?: boolean,
    setErrorState?: React.Dispatch<React.SetStateAction<boolean>>) => {
    if(input === "") {
      setState(0);
      return;
    }
    const numberInput = Number(input);
    if(precision && setErrorState){
      if(/^\d*$/.test(input) && numberInput <= 4){
          setState(numberInput);
          setErrorState(false);
      } else {
        setErrorState(true);
      }
    } 
    else {
      if(/^\d*$/.test(input)) {
        setState(numberInput)
      }
    }
  };