import React, { useState, useEffect } from 'react';
import { Box, Slider, Typography, TextField, Stack, InputAdornment } from '@mui/material';
import { PriceRange } from '../../../types/types';

interface PriceRangeSliderProps {
  value: PriceRange;
  onChange: (priceRange: PriceRange) => void;
}

const PriceRangeSlider: React.FC<PriceRangeSliderProps> = ({ value, onChange }) => {
  const [sliderValue, setSliderValue] = useState<number[]>([value.min, value.max]);

  useEffect(() => {
    setSliderValue([value.min, value.max]);
  }, [value]);

  const handleSliderChange = (event: Event, newValue: number | number[]) => {
    setSliderValue(newValue as number[]);
  };

  const handleSliderChangeCommitted = (event: React.SyntheticEvent | Event, newValue: number | number[]) => {
    const updatedValue = newValue as number[];
    onChange({ min: updatedValue[0], max: updatedValue[1] });
  };

  return (
    <Box sx={{ width: '100%' }}>
      <Box sx={{ marginLeft: '15px', width: '260px' }}>
        <Slider
          value={sliderValue}
          onChange={handleSliderChange}
          onChangeCommitted={handleSliderChangeCommitted}
          valueLabelDisplay="auto"
          aria-labelledby="range-slider"
          min={0}
          max={50000}
        />
      </Box>
      <Stack spacing={1} direction="row" alignItems="center" justifyContent="center">
        <Box sx={{ minWidth: 80  }}>
          <TextField
            size="small"
            value={value.min}
            onChange={(e) => onChange({ ...value, min: parseInt(e.target.value, 10) || 0 })}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <Typography variant="body2">$USD&nbsp;</Typography>
                </InputAdornment>
              ),
              inputProps: { min: 0, max: value.max, type: 'number' },
            }}
          />
        </Box>
        <Box sx={{ minWidth: 80 }}>
          <TextField
            size="small"
            value={value.max}
            onChange={(e) => onChange({ ...value, max: parseInt(e.target.value, 10) || 50000 })}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <Typography variant="body2">$USD&nbsp;</Typography>
                </InputAdornment>
              ),
              inputProps: { min: value.min, max: 50000, type: 'number' },
            }}
          />
        </Box>
      </Stack>
    </Box>
  );
};

export default PriceRangeSlider;
