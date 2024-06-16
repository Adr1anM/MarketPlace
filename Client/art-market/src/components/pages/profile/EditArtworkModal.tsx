import { 
  Modal, 
  Box, 
  Typography, 
  TextField, 
  Button, 
  IconButton, 
  Stack,Theme, 
  useTheme, 
  SelectChangeEvent, 
  FormControl, 
  InputLabel, 
  Select, 
  OutlinedInput, 
  MenuItem, 
}  from "@mui/material";
import { Artwork, Category } from "../../../types/types";
import { memo, useEffect, useState } from "react";
import CloseIcon from '@mui/icons-material/Close';
import InputFileUpload from "./InputFileUpload";
import useArtworks from "../../../zsm/stores/useArtworks";
import React from "react";
import useSubCategories from "../../../zsm/stores/useSubCategory";
import useCategories from "../../../zsm/stores/useCategory";

const ITEM_HEIGHT = 20;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};


function getStyles(name: string, selectedCategory: string[], theme: Theme) {
  return {
    fontWeight:
    selectedCategory.indexOf(name) === -1
        ? theme.typography.fontWeightRegular
        : theme.typography.fontWeightMedium,
    backgroundColor:
    selectedCategory.indexOf(name) !== -1 ? theme.palette.primary.light : "inherit",
    color:
    selectedCategory.indexOf(name) !== -1 ? theme.palette.primary.contrastText : "inherit",    
  };
}

interface EditArtworkModalProps {
  open: boolean;
  onClose: () => void;
  artwork: Artwork;
}

const EditArtworkModal = memo<EditArtworkModalProps>(({ open, onClose, artwork }) => {

  const theme = useTheme();
  const artworkStore = useArtworks();  
  const subCategoryStore = useSubCategories();
  const categoryStore = useCategories();
  const [updatedArtwork, setUpdatedArtwork] = useState<Artwork>(artwork);

  const [subCategoryName, setSubCategoryName] = React.useState<string[]>([]);
  const [selectedSubCategoryIds, setSelectedSubCategoryIds] = useState<number[]>(
    artwork.subCategoryIds || []
  );

  const [category, setCategory] = React.useState("");

  const handleCategoryChange = (event: SelectChangeEvent) => {
    setCategory(event.target.value);
    setUpdatedArtwork((prevArtwork) => ({
      ...prevArtwork,
      categoryID: categoryStore.categories.find(categ => categ.name === event.target.value)?.id || 0,
    }));
  };

  
  useEffect(() => {
    subCategoryStore.fetchCategories(); 
    categoryStore.fetchCategories()
  }, []);

  useEffect(() => {
    if (artwork.subCategoryIds) {
      const initialSelectedNames = artwork.subCategoryIds.map((id) => {
        const category = subCategoryStore.categories.find((cat) => cat.id === id);
        return category ? category.name : "";
      });
      setSubCategoryName(initialSelectedNames);
    }

    if (artwork.categoryID) {
      const initialCategory = categoryStore.categories.find((cat) => cat.id === artwork.categoryID)?.name || '';
      setCategory(initialCategory);
    }
  }, [artwork,, subCategoryStore.categories,categoryStore.categories]);


  const handleChangee = (event: SelectChangeEvent<typeof subCategoryName>) => {
    const {
      target: { value },
    } = event;
    const selectedCategoryNames = typeof value === "string" ? value.split(",") : value

    const selectedCategoryIds = selectedCategoryNames.map((subCategoryName) => {
      const category = subCategoryStore.categories.find((cat) => cat.name === subCategoryName);
      return category ? category.id : null;
    }).filter(id => id !== null) as number[];

    setSelectedSubCategoryIds(selectedCategoryIds);
    setSubCategoryName(selectedCategoryNames);
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setUpdatedArtwork((prevArtwork) => ({
      ...prevArtwork,
      [name]: value,
    }));
  };

  const handleSubmit = async () => {
    try {
      const updatedEntity = {
        ...updatedArtwork,
        subCategoryIds: selectedSubCategoryIds,
      };
      console.log("Updated entity:",updatedEntity);

        await artworkStore.updateArtwork(updatedEntity);
       console.log("category id:",artwork.categoryID);
       console.log("Updated Artwork:",updatedArtwork);

        onClose(); 
    } catch (error) {
        console.error('Error updating artwork:', error);
    }
};


const handleUpload = (file: File) => {
    try {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const base64String = reader.result as string;
        const updatedEntity = { 
          ...updatedArtwork!, 
          imageData: base64String.replace(/^data:image\/[a-z]+;base64,/, "")
        };
        setUpdatedArtwork(updatedEntity);
      };
    } catch (error) {
      console.error('Error uploading image:', error);
    }
};


  return (
    <Modal open={open} onClose={onClose}>
      <Box sx={{ 
        position: 'absolute', 
        top: '50%', left: '50%', 
        transform: 'translate(-50%, -50%)', 
        bgcolor: 'background.paper', 
        boxShadow: 24, 
        p: 4,
        width: '600px',
        height: '750px',
        maxWidth: '90%', 
        maxHeight: '90%', 
        overflowY: 'auto',
        }}
      >
      <Typography variant="h6" component="h2">
          Edit Artwork
      </Typography>
      <IconButton
            aria-label="close"
            onClick={() => {onClose()}} 
            sx={{
                position: 'absolute',
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
                outline: 'none',
                '&:focus': {outline: 'none'}
            }}
        >
        <CloseIcon />
        </IconButton>
        {artwork?.imageData ? (
              <Box
                component="img"
                sx={{
                    height: 200,
                    width: 200,
                    objectFit: 'cover'
                }}
                alt="The house from the offer."
                src={`data:image/jpeg;base64,${updatedArtwork?.imageData?.toString()}`}
              />
            ) : (
              <Box
                component="img"
                sx={{
                    height: 200,
                    width: 200,
                    objectFit: 'cover'
                }}
                alt="The house from the offer."
                src={"https://t4.ftcdn.net/jpg/05/17/53/57/360_F_517535712_q7f9QC9X6TQxWi6xYZZbMmw5cnLMr279.jpg"}
              />
            )}
        <Box >
            <Stack sx={{marginTop: '15px'}} direction="row" gap="10px">
                <Button>Delete</Button>
                <InputFileUpload onUpload={handleUpload} />
            </Stack>    
        </Box>    

        <Stack direction="column">
        <TextField
          label="Title"
          name="title"
          value={updatedArtwork.title}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Description"
          name="description"
          value={updatedArtwork.description}
          onChange={handleChange}
          fullWidth
          margin="normal"
          multiline
        />
        <TextField
          label="Price"
          name="price"
          type="number"
          value={updatedArtwork.price}
          onChange={handleChange}
          fullWidth
          margin="normal"
        />

        <FormControl sx={{  marginTop: '15px', width: 300 }}>
          <InputLabel id="demo-simple-select-helper-label">Category</InputLabel>
          <Select
            labelId="demo-simple-select-helper-label"
            id="demo-simple-select-helper"
            value={category}
            label="Category"
            onChange={handleCategoryChange}
          >
            {categoryStore.categories.map((categ: Category ) =>(
              <MenuItem
                key={categ.id}
                value={categ.name}
              >
                {categ.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControl sx={{ marginTop: '25px', width: 300 }}>
          <InputLabel id="demo-multiple-name-label">SubCategory</InputLabel>
          <Select
            labelId="demo-multiple-name-label"
            id="demo-multiple-name"
            multiple
            value={subCategoryName}
            onChange={handleChangee}
            input={<OutlinedInput label="Name" />}
            MenuProps={MenuProps}
          >
            {subCategoryStore.categories.map((category: Category) => (
                <MenuItem
                  key={category.id}
                  value={category.name}
                  style={getStyles(category.name, subCategoryName, theme)}
                >
                  {category.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <Button sx={{marginTop: '15px',width: '50px'}} onClick={handleSubmit} variant="contained" color="primary">
          Update
        </Button>
        </Stack>
        
      </Box>
    </Modal>
  );
});

export default EditArtworkModal;