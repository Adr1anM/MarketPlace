import { Box, Card, Fab, Grid, IconButton, Link, Paper, Typography } from "@mui/material";
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart'
import { Artwork, User } from "../../../types/types";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { useState, useEffect } from "react";
import EditArtworkModal from "./EditArtworkModal";
import { Link as RouterLink } from 'react-router-dom'; 
import useShoppingCart from "../../../zsm/stores/useShoppingCart";
import toast from "react-hot-toast";
import LogInModal from "../../layouts/navbar/navbarLogin/LogInModal";

interface ArtworkCardProps {
    artwork: Artwork;
    onDelete: (id:number) => void;
    user: User | undefined
}

const ArtworkCard: React.FC<ArtworkCardProps> = ({ artwork , onDelete, user}) =>{

  const cartStore = useShoppingCart();
  const [openEditModal, setOpenEditModal] = useState(false);
  const [isInCart, setIsInCart] = useState(false);
  const [loginModal, setloginModal] = useState(false);


  useEffect(() => {
    const cartItem = cartStore.getCartItemById(artwork.id);
    if(!!cartItem && user){
      setIsInCart(!!cartItem);
    }
  }, [cartStore, artwork.id, user]);

  const handleOpenEditModal = () => {
    setOpenEditModal(true);
  };

  const handleCloseEditModal = () => {
    setOpenEditModal(false);
  };
  const handleEdit = () => {
    handleOpenEditModal();
  };

  const handleAddTocart = async () => {
    if(!user){
      handleOpen();
    }else{
      const item = {
          userId: user?.id || 0,
          productId: artwork.id,
          quantity: 1  
        };

      if (!isInCart) {
        await cartStore.addToCart(item);
        toast.success("Product added in the cart");
        setIsInCart(true); 
      }
    }
  };

  function handleOpen() {
    setloginModal(true);
  }

  function handleLoginModalClose() {
    setloginModal(false);
  }


  const handleDelete = () => {
    onDelete(artwork.id);
  };

  return (
    <>
      <Card elevation={0} sx={{ bgcolor: 'transparent', maxWidth: 345 , maxHeight: 400 }} >            
        <Link component={RouterLink} to={`/artwork/${artwork.id}`}>
          <Paper elevation={16}
            sx={{ 
              height: '250px', 
              borderRadius: '15px',
              backgroundImage: `url(data:image/jpeg;base64,${artwork?.imageData})`,
              backgroundSize: 'cover',
              backgroundPosition: 'center'
            }}  
            component="div"
          ></Paper>   
        </Link>

        <Box sx={{bgcolor: 'transparent', paddingTop: '10px'}}>
          <Grid container sx={{ width: '100%', height: '100%'}}>

            <Grid item xs={12}>
              <Link component={RouterLink} to={`/author/${artwork.authorId}`}>
                <Typography component={'h5'} >
                  {artwork.firstName + " " + artwork.lastName}
                </Typography>
              </Link>
            </Grid>
            <Grid item xs={12}>
              <Typography>
                {artwork.title}
              </Typography>
            </Grid>

            <Grid item xs={12}>
              <Typography>
                {new Date(artwork.createdDate).toLocaleDateString()}
              </Typography>
            </Grid>
            {user?.roles.includes("Author") && user.lastName === artwork.lastName ? (
              <Grid container>
                <Grid sx={{position: 'relative'}} item xs={6} md={8}>
                  <Typography sx={{position: 'absolute',bottom: '3px'}}>
                    {artwork.price}$
                  </Typography>
                </Grid>

                <Grid item xs={6} md={2}>
                  <IconButton sx={{ '&:focus': {outline: 'none' } }} aria-label="delete" size="medium" onClick={handleDelete}>
                    <DeleteIcon fontSize="inherit" />
                  </IconButton>
                </Grid>
                <Grid item xs={6} md={2}>
                  <Fab sx={{ '&:focus': {outline: 'none' },transform: 'scale(0.8)' }} color="secondary" aria-label="edit" size="small" onClick={handleEdit}>
                    <EditIcon />
                  </Fab>
                </Grid> 
              </Grid>    
            ) : (
              <Grid container>
                <Grid sx={{position: 'relative'}} item xs={6} md={9}>
                  <Typography sx={{position: 'absolute',bottom: '3px'}}>
                    {artwork.price}$
                  </Typography>
                </Grid>

                <Grid item xs={6} md={3}>
                  <IconButton
                    sx={{ '&:focus': {outline: 'none' } }}
                    color="primary"
                    aria-label="add to shopping cart"
                    onClick={handleAddTocart}
                    disabled={isInCart}  
                  >
                    <AddShoppingCartIcon />
                  </IconButton>
                </Grid>
              </Grid>   
            )}   
          </Grid>
        </Box>
      </Card>
      <EditArtworkModal open={openEditModal} onClose={handleCloseEditModal} artwork={artwork}/>
      <LogInModal isOpened={loginModal} onClose={handleLoginModalClose} title="LgIn or Register to save artworks"/>
    </>
  );
}

export default ArtworkCard;
