import {
    Modal,
    Box,
    Typography,
    TextField,
    Button,
    IconButton,
    Stack,
    Theme,
    useTheme,
    SelectChangeEvent,
    FormControl,
    InputLabel,
    Select,
    OutlinedInput,
    MenuItem,
  } from "@mui/material";
  import { Category, CategoryWithSubcategories, CreateArtwork } from "../../../types/types";
  import { memo, useEffect, useState } from "react";
  import CloseIcon from '@mui/icons-material/Close';
  import InputFileUpload from "./InputFileUpload";
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
    onCreate: (newArtwork: CreateArtwork) => void;
    authorID: number;
  }
  
  const CreateArtworkModal = memo<EditArtworkModalProps>(({ open, onClose, onCreate, authorID  }) => {
  
    const theme = useTheme();
    const subCategoryStore = useSubCategories();
    const categoryStore = useCategories();
    const [newArtwork, setNewArtwork] = useState<CreateArtwork>({
      title: '',
      description: '',
      categoryID: 0,
      authorId: authorID,
      quantity: 1,
      price: 0,
      createdDate: '',  
      imageData: null,
      subCategoryIds: [],
    });

    const resetForm = () => {
      setNewArtwork({
        title: '',
        description: '',
        categoryID: 0,
        authorId: authorID, 
        quantity: 1,
        price: 0,
        createdDate: '',
        imageData: null,
        subCategoryIds: [],
      });
      setSubCategoryName([]); 
      setSelectedSubCategoryIds([]); 
      setCategory(''); 
      setErrors({}); 
    };
  
    const [subCategoryName, setSubCategoryName] = React.useState<string[]>([]);
    const [selectedSubCategoryIds, setSelectedSubCategoryIds] = useState<number[]>([]);
    const [category, setCategory] = React.useState('');
    const [errors, setErrors] = useState<{ [key: string]: string }>({});
  
    const handleCategoryChange = (event: SelectChangeEvent) => {
      setCategory(event.target.value);
      setNewArtwork((prevArtwork) => ({
        ...prevArtwork,
        categoryID: categoryStore.categories.find(categ => categ.categoryName === event.target.value)?.categoryId || 0,
      }));
    };
  
    useEffect(() => {
      subCategoryStore.fetchSubCategories();
      categoryStore.fetchCategories();
    }, []);
  
    const handleChangee = (event: SelectChangeEvent<typeof subCategoryName>) => {
      const {
        target: { value },
      } = event;
      const selectedCategoryNames = typeof value === "string" ? value.split(",") : value
  
      const selectedCategoryIds = selectedCategoryNames.map((subCategoryName) => {
        const category = subCategoryStore.subCategories.find((subCat) => subCat.name === subCategoryName);
        return category ? category.id : null;
      }).filter(id => id !== null) as number[];
  
      setSelectedSubCategoryIds(selectedCategoryIds);
      setSubCategoryName(selectedCategoryNames);
      setNewArtwork((prevArtwork) => ({
        ...prevArtwork,
        subCategoryIds: selectedCategoryIds
      }));
    };
  
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
      const { name, value } = event.target;
      setNewArtwork((prevArtwork) => ({
        ...prevArtwork,
        [name]: value,
        authorId: authorID,
        createdDate: new Date().toISOString(),
      }));
    };
  
    const validateForm = () => {
      const newErrors: { [key: string]: string } = {};
  
      if (!newArtwork.title) newErrors.title = "Title is required";
      if (!newArtwork.description) newErrors.description = "Description is required";
      if (newArtwork.price <= 0) newErrors.price = "Price must be greater than zero";
      if (newArtwork.categoryID === 0) newErrors.category = "Category is required";
      if (newArtwork.subCategoryIds.length === 0) newErrors.subCategories = "At least one subcategory is required";
      if (!newArtwork.imageData) newErrors.imageData = "Image upload is required";
  
      setErrors(newErrors);
      return Object.keys(newErrors).length === 0;
    };
  
    const handleSubmit = () => {
      if (!validateForm()) {
        return;
      }
      try {
        const createdEntity = {
          ...newArtwork,
         
        };
        console.log("Created entity:", createdEntity);
  
        onCreate(createdEntity);
        console.log("Created Artwork:", newArtwork);
  
        onClose();
      } catch (error) {
        console.error('Error creating artwork:', error);
      }
    };
  
    const handleUpload = (file: File) => {
      try {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          const base64String = reader.result as string;
          const updatedEntity = {
            ...newArtwork!,
            imageData: base64String.replace(/^data:image\/[a-z]+;base64,/, "")
          };
          setNewArtwork(updatedEntity);
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
            Create Artwork
          </Typography>
          <IconButton
            aria-label="close"
            onClick={() => { onClose() }}
            sx={{
              position: 'absolute',
              right: 8,
              top: 8,
              color: (theme) => theme.palette.grey[500],
              outline: 'none',
              '&:focus': { outline: 'none' }
            }}
          >
            <CloseIcon />
          </IconButton>
          
          {errors.imageData && <Typography color="error">{errors.imageData}</Typography>}
          {newArtwork?.imageData ? (
            <Box
              component="img"
              sx={{
                height: 200,
                width: 200,
                objectFit: 'cover'
              }}
              alt="The house from the offer."
              src={`data:image/jpeg;base64,${newArtwork?.imageData?.toString()}`}
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
          <Box>
            <Stack sx={{ marginTop: '15px' }} direction="row" gap="10px">
              <Button>Delete</Button>
              <InputFileUpload onUpload={handleUpload} />
            </Stack>
          </Box>
          <Stack direction="column">
            <TextField
              label="Title"
              name="title"
              value={newArtwork.title}
              onChange={handleChange}
              fullWidth
              margin="normal"
              error={!!errors.title}
              helperText={errors.title}
              required
            />
            <TextField
              label="Description"
              name="description"
              value={newArtwork.description}
              onChange={handleChange}
              fullWidth
              margin="normal"
              multiline
              error={!!errors.description}
              helperText={errors.description}
              required
            />
            <TextField
              sx={{width: 300}}
              label="Price"
              name="price"
              type="number"
              value={newArtwork.price}
              onChange={handleChange}
              fullWidth
              margin="normal"
              error={!!errors.price}
              helperText={errors.price}
              required
            />
  
  <FormControl sx={{ marginTop: "15px", width: 300 }} error={!!errors.category}>
              <InputLabel id="demo-simple-select-helper-label">Category</InputLabel>
              <Select
                labelId="demo-simple-select-helper-label"
                id="demo-simple-select-helper"
                value={category}
                label="Category"
                onChange={handleCategoryChange}
                required
              >
                {categoryStore.getCategories().map((categ: CategoryWithSubcategories) => (
                  <MenuItem key={categ.categoryId} value={categ.categoryName}>
                    {categ.categoryName}
                  </MenuItem>
                ))}
              </Select>
              {errors.category && (
                <Typography color="error">{errors.category}</Typography>
              )}
            </FormControl>
  
            <FormControl sx={{ marginTop: '25px', width: 300 }} error={!!errors.subCategories}>
              <InputLabel id="demo-multiple-name-label">SubCategory</InputLabel>
              <Select
                labelId="demo-multiple-name-label"
                id="demo-multiple-name"
                multiple
                value={subCategoryName}
                onChange={handleChangee}
                input={<OutlinedInput label="Name" />}
                MenuProps={MenuProps}
                required
              >
                {subCategoryStore.subCategories.map((category: Category) => (
                  <MenuItem
                    key={category.id}
                    value={category.name}
                    style={getStyles(category.name, subCategoryName, theme)}
                  >
                    {category.name}
                  </MenuItem>
                ))}
              </Select>
              {errors.subCategories && <Typography color="error">{errors.subCategories}</Typography>}
            </FormControl>
          </Stack>

          <Stack direction="row">
            <Button sx={{ marginTop: '15px', width: '50px' }} onClick={handleSubmit} variant="contained" color="primary">
              Create
            </Button> 
            <Button sx={{
               marginLeft: '15px',
               marginTop: '15px', 
               width: '50px' 
              }} 
              variant="outlined" 
              color="error" 
              onClick={() => {
              resetForm();
              onClose();
              }}
            >
              Cancel
            </Button>
          </Stack>
           
        </Box>
      </Modal>
    );
  });
  
  export default CreateArtworkModal;
  