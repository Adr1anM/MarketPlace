import { Box, Card, Fab, Grid, IconButton, Link, Paper, Typography } from "@mui/material";
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart'
import { Artwork, User } from "../../../types/types";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { useState } from "react";
import EditArtworkModal from "./EditArtworkModal";

interface ArtworkCardProps {
    artwork: Artwork;
    onDelete: (id:number) => void;
    user: User | undefined
  }


const ArtworkCard: React.FC<ArtworkCardProps> = ({ artwork , onDelete, user}) =>{

  const [openEditModal, setOpenEditModal] = useState(false);

  const handleOpenEditModal = () => {
    setOpenEditModal(true);
  };

  const handleCloseEditModal = () => {
    setOpenEditModal(false);
  };
  console.log("artwork",artwork);
  const handleEdit = () => {
     
     handleOpenEditModal();
  };

  const handleDelete = () => {
    onDelete(artwork.id);
  };
 
    return (
        <>
            <Card elevation={0} sx={{ bgcolor: 'transparent', maxWidth: 345 , maxHeight: 400 }} >            

                <Link href="#">
                    <Paper  elevation={16}
                        sx={{ 
                            height: '250px', 
                            borderRadius: '15px',
                            backgroundImage: `url(data:image/jpeg;base64,${artwork?.imageData})`,
                            backgroundSize: 'cover',
                            backgroundPosition: 'center'
                        }}  
                        component="div"
                    >
                    
                    </Paper>   
                </Link>

                <Box  sx={{bgcolor: 'transparent', paddingTop: '10px'}}>
                    <Grid container  sx={{ width: '100%', height: '100%'}}>

                        <Grid  item xs={12} >
                            <Link href="#">
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
            
                        <Grid item xs={12} >
                            <Typography>
                                {new Date(artwork.createdDate).toLocaleDateString()}
                            </Typography>
                        </Grid>
                        {user?.roles.includes("Author") ?(
                        <Grid container>
                            <Grid sx={{position: 'relative'}} item xs={6}  md = {8} >
                                <Typography sx={{position: 'absolute',bottom: '3px', }}>
                                {artwork.price}$
                                </Typography>
                            </Grid>

                            <Grid  item xs={6}  md={2}>
                                <IconButton sx={{ '&:focus': {outline: 'none' } }} aria-label="delete" size="medium" onClick={handleDelete}>
                                    <DeleteIcon  fontSize="inherit" />
                                </IconButton>
                            </Grid>
                            <Grid  item xs={6}  md={2}>
                                <Fab sx={{ '&:focus': {outline: 'none' },transform: 'scale(0.8)' }} color="secondary" aria-label="edit" size="small" onClick={handleEdit}>
                                    <EditIcon  />
                                </Fab>
                            </Grid> 
                        </Grid>    
                        ):(
                        <Grid container>
                            <Grid sx={{position: 'relative'}} item xs={6}  md = {9} >
                                <Typography sx={{position: 'absolute',bottom: '3px', }}>
                                {artwork.price}$
                                </Typography>
                            </Grid>

                            <Grid  item xs={6}  md={3}>
                                <IconButton sx={{ '&:focus': {outline: 'none' } }}   color="primary" aria-label="add to shopping cart">
                                    <AddShoppingCartIcon />
                                </IconButton>
                            </Grid>
                        </Grid>   
                    )}   
                    </Grid>
                </Box>
            </Card>
            <EditArtworkModal open={openEditModal} onClose={handleCloseEditModal} artwork={artwork}/>
        </>
    );
}


export default ArtworkCard;