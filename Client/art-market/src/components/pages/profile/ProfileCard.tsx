// import { Box, Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Grid, IconButton, Link, Paper, Typography } from "@mui/material";
// import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart'

// interface ArtworkCardProps {
//     artwork: {
//       id: number;
//       title: string;
//       description: string;
//       price: number;
//       createdDate: string;
//       userId: number;
//       firstName: string;
//       lastName: string;
  
//     };
//   }


// const ProfileCard: React.FC<ArtworkCardProps> = ({ artwork }) =>{
//     return (
//         <>
//             <Card elevation={24} sx={{ borderRadius: '15px', bgcolor: 'transparent', maxWidth: 345 , maxHeight: 400 }} >            

//                 <Link href="#">
//                     <Box sx={{height: '250px'}}  component="img" src="https://img.freepik.com/free-photo/colorful-design-with-spiral-design_188544-9588.jpg" alt="MateLabs machine learning" >

//                     </Box>  
//                 </Link>

//                 <Box sx={{bgcolor: 'transparent'}}>
//                     <Grid container sx={{padding: '5px', width: '100%', height: '100%'}}>

//                         <Grid item xs={6}  md={9}>
//                             <Link href="#">
//                                 <Typography component={'h5'}>
//                                     {artwork.firstName + " " + artwork.lastName}
//                                 </Typography>
//                             </Link>
//                         </Grid>
//                         <Grid item xs={6}  md={3}>
//                             <IconButton color="primary" aria-label="add to shopping cart">
//                                 <AddShoppingCartIcon />
//                             </IconButton>
//                         </Grid>

//                         <Grid item xs={12}>
//                             <Typography>
//                                 {artwork.title}
//                             </Typography>
//                         </Grid>
            
//                         <Grid item xs={12} >
//                             <Typography>
//                                 {new Date(artwork.createdDate).toLocaleDateString()}
//                             </Typography>
//                         </Grid>
//                         <Grid item xs={12} >
//                             <Typography>
//                                 {artwork.price}$
//                             </Typography>
//                         </Grid>
//                     </Grid>
//                 </Box>
//             </Card>
//         </>
//     );
// }


// export default ProfileCard;


import { Box, Button, Card, CardActionArea, CardActions, CardContent, CardMedia, Grid, IconButton, Link, Paper, Typography } from "@mui/material";
import AddShoppingCartIcon from '@mui/icons-material/AddShoppingCart'

interface ArtworkCardProps {
    artwork: {
      id: number;
      title: string;
      description: string;
      price: number;
      createdDate: string;
      userId: number;
      firstName: string;
      lastName: string;
  
    };
  }


const ProfileCard: React.FC<ArtworkCardProps> = ({ artwork }) =>{
    return (
        <>
            <Card elevation={0} sx={{ bgcolor: 'transparent', maxWidth: 345 , maxHeight: 400 }} >            

                <Link href="#">
                    <Paper  elevation={16}
                        sx={{ 
                            height: '250px', 
                            borderRadius: '15px',
                            backgroundImage: 'url(https://img.freepik.com/free-photo/colorful-design-with-spiral-design_188544-9588.jpg)',
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
                        <Grid sx={{position: 'relative'}} item xs={6}  md = {9} >
                            <Typography sx={{position: 'absolute',bottom: '3px', }}>
                                {artwork.price}$
                            </Typography>
                        </Grid>
                        <Grid  item xs={6}  md={3}>
                            <IconButton sx={{ '&:focus': {outline: 'none' } }}  color="primary" aria-label="add to shopping cart">
                                <AddShoppingCartIcon />
                            </IconButton>
                        </Grid>
                    </Grid>
                </Box>
            </Card>
        </>
    );
}


export default ProfileCard;