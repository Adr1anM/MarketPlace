import React from 'react';
import { Box, Accordion, AccordionSummary, AccordionDetails, Divider, Stack, Checkbox, Typography, FormControl, FormGroup, FormControlLabel, FormLabel, IconButton, AccordionActions, Button } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import PriceRangeSlider from './PriceRangeSlider';
import CloseIcon from '@mui/icons-material/Close';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AuthorNames, DateRange, PriceRange, SelectedOptions} from '../../../types/types';
import useCategories from '../../../zsm/stores/useCategory';
import dayjs, { Dayjs } from 'dayjs';
import AuthorsName from './AuthorsName';

interface DrawerListProps {
toggleDrawer: (newOpen: boolean) => () => void;
countries : string[] | null | undefined;
authorNames : AuthorNames[] | null | undefined;
selectedOptions: SelectedOptions;
setSelectedOptions: React.Dispatch<React.SetStateAction<SelectedOptions>>;
resetFilterOptions: () => void;
}
const label = { inputProps: { 'aria-label': 'Checkbox demo' } };

const DrawerList: React.FC<DrawerListProps> = ({ toggleDrawer, countries, authorNames,selectedOptions,setSelectedOptions ,resetFilterOptions}) => {

const categoryStore = useCategories();

const [checked, setChecked] = React.useState(false);

const handleCheckboxChange = (option: keyof SelectedOptions, value: string, isChecked: boolean) => {
    setSelectedOptions((prevOptions) => {
      const updatedOptions = { ...prevOptions };
      

      if (option === "categories") {
        if (isChecked) {    
          updatedOptions[option].push(value);
        } else {
          updatedOptions[option] = updatedOptions[option].filter((item) => item !== value);
        }
      } else if(option === "countries") {
        if (isChecked) {
            updatedOptions[option].push(value);
          } else {  
            updatedOptions[option] = updatedOptions[option].filter((item) => item !== value);
          }
      }
      return updatedOptions;
    });
  };

  const handleauthorNameChange = (author: AuthorNames | null) =>{
    if (author) {
        setSelectedOptions((prevOptions) => ({
          ...prevOptions,
          authors: [author],
        }));
      }
  }

  const handlePriceRangeChange = (priceRange: PriceRange ) => {
    setSelectedOptions((prevOptions) => ({
      ...prevOptions,
      priceRange: priceRange,
    }));
  };

  const handleDateChange = (field: keyof DateRange, newValue: Dayjs | null) => {
    setSelectedOptions((prevOptions) => ({
      ...prevOptions,
      dateRange: {
        ...prevOptions.dateRange,
        [field]: newValue ? newValue.format('YYYY-MM-DD') : null,
      } as DateRange,
    }));
  };
return (
    <Box sx={{ width: 350 ,padding: 2}} role="presentation">
        <IconButton
                aria-label="close"
                onClick={toggleDrawer(false)}
                sx={{
                    position: 'absolute',
                    right: 8,
                    top: 8,
                    zIndex: 1,
                    color: (theme) => theme.palette.grey[500],
                    outline: 'none',
            }}
            >
            <CloseIcon />
        </IconButton>
   
        <Accordion  defaultExpanded>
            <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1-content" id="panel1-header">
                <Typography fontSize={20}>
                    Authors
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
                <AuthorsName authors={authorNames?? []} onAuthorSelect={handleauthorNameChange}/>
            </AccordionDetails>
        </Accordion>
        <Accordion defaultExpanded>
            <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel2-content" id="panel2-header">
                <Typography fontSize={20}>
                    Price
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
            <PriceRangeSlider value={selectedOptions.priceRange} onChange={handlePriceRangeChange} />
            </AccordionDetails>
        </Accordion>
        <Accordion defaultExpanded>
            <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1-content" id="panel1-header">
                <Typography fontSize={20}>
                    Medium
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
                <Stack  direction="column" alignItems="flex-start" justifyContent="flex-start" spacing={1}>
                <FormControl component="fieldset">                 
                    <FormGroup >
                        {categoryStore.categories?.map((category) => (
                            <FormControlLabel
                            value={category.categoryName} 
                            control={<Checkbox checked={selectedOptions.categories.includes(category.categoryName)}
                            onChange={(e) => handleCheckboxChange("categories", category.categoryName, e.target.checked)}/>}
                            label={category.categoryName}
                            labelPlacement="end"
                        />
                        ))}
                    </FormGroup>                          
                </FormControl>
                </Stack>
            </AccordionDetails>
        </Accordion>
        <Accordion  defaultExpanded>
            <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1-content" id="panel1-header">
                <Typography fontSize={20}>
                    Authors's Country
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
                <Stack  direction="column" alignItems="flex-start" justifyContent="flex-start" spacing={1}>
                <FormControl component="fieldset">
                    <FormGroup >
                        {countries?.map((country) => (
                            <FormControlLabel
                                value={country}
                                control={<Checkbox checked={selectedOptions.countries.includes(country)}
                                onChange={(e) => handleCheckboxChange("countries", country, e.target.checked)} />}
                                label={country}
                                labelPlacement="end"
                             />
                        ))}
                    </FormGroup>
                </FormControl>
                </Stack>
            </AccordionDetails>
        </Accordion>
        <Accordion defaultExpanded>
            <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1-content" id="panel1-header">
                <Typography fontSize={20}>
                    Date
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
                <Stack direction="row" gap={5}>
                     <Typography sx={{display: 'flex',alignItems: 'center', justifyContent: 'center'}}>
                        From
                    </Typography>
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <DemoContainer components={['DatePicker']}>
                            <DatePicker
                                label="Start date"
                                value={selectedOptions.dateRange?.start ? dayjs(selectedOptions.dateRange.start) : null}
                                onChange={(newValue) => handleDateChange('start', newValue)}
                            />
                        </DemoContainer>
                    </LocalizationProvider>
                   
                </Stack>
                <Stack direction="row" gap={7.5}>
                     <Typography sx={{display: 'flex',alignItems: 'center', justifyContent: 'center'}}>
                        To
                    </Typography>
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <DemoContainer components={['DatePicker']}>
                            <DatePicker
                                label="End date"
                                value={selectedOptions.dateRange?.end ? dayjs(selectedOptions.dateRange.end) : null}
                                onChange={(newValue) => handleDateChange('end', newValue)}
                            />
                        </DemoContainer>
                    </LocalizationProvider>
                   
                </Stack>
            </AccordionDetails>
            <AccordionActions>
                <Button onClick={() => resetFilterOptions()}>Reste Filter</Button>
            </AccordionActions>
        </Accordion>
        <Divider sx={{ mt: 2 }} />
    </Box>
);
};

export default DrawerList;