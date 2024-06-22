import React from 'react';
import { Button, Popover, FormControl, RadioGroup, FormControlLabel, Radio } from '@mui/material';


interface SortPopoverProps {
    anchorEl: HTMLButtonElement | null;
    handleClick: (event: React.MouseEvent<HTMLButtonElement>) => void;
    handleClose: () => void;
    sortDirection: "asc" | "desc";
    handleSortDirectionChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const SortPopover: React.FC<SortPopoverProps> = ({ anchorEl, handleClick, handleClose, sortDirection, handleSortDirectionChange }) => {
    const opeN = Boolean(anchorEl);
    const id = opeN ? 'simple-popover' : undefined;

    return (
        <div>
            <Button className='button' aria-describedby={id} variant="contained" onClick={handleClick}>
                Sort
            </Button>
            <Popover
                id={id}
                open={opeN}
                anchorEl={anchorEl}
                onClose={handleClose}
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'left',
                }}
            >
                <FormControl>
                    <RadioGroup
                        aria-labelledby="demo-radio-buttons-group-label"
                        value={sortDirection}
                        onChange={handleSortDirectionChange}
                        name="radio-buttons-group"
                    >
                        <FormControlLabel value="asc" control={<Radio />} label="Ascending" />
                        <FormControlLabel value="desc" control={<Radio />} label="Descending" />
                    </RadioGroup>
                </FormControl>
            </Popover>
        </div>
    );
};

export default SortPopover;