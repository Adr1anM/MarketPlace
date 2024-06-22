import { FormControl, InputLabel, MenuItem, Select, SelectChangeEvent } from "@mui/material";
import { Category } from "../../../../types/types";

interface CategorySelectorProps {
  category: string;
  categories: Category[];
  handleCategoryChange: (event: SelectChangeEvent<string>) => void;
}

const CategorySelector = ({ category, categories, handleCategoryChange }: CategorySelectorProps) => (
  <FormControl sx={{ marginTop: "15px", width: 300 }}>
    <InputLabel id="demo-simple-select-helper-label">Category</InputLabel>
    <Select
      labelId="demo-simple-select-helper-label"
      id="demo-simple-select-helper"
      value={category}
      label="Category"
      onChange={handleCategoryChange}
    >
      {categories.map((categ) => (
        <MenuItem key={categ.id} value={categ.name}>
          {categ.name}
        </MenuItem>
      ))}
    </Select>
  </FormControl>
);

export default CategorySelector;
