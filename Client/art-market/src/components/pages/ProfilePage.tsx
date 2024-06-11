import { Avatar, Box, Button, Card, CardActions, CardContent, CardHeader, Collapse, Container, Fab, FormControl, Grid, IconButton, Modal, Paper, SxProps, TextField, Typography, Zoom } from "@mui/material";
import './pagesStyles/ProfilePage.css';
import { useState } from "react";
import CloseIcon from '@mui/icons-material/Close';
import { Artwork, Author, User } from "../../types/types";
import ProfileCard from "./profile/ProfileCard";
import { dummyArtworks } from "../../dummyDataStore/artworksData";
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';

const user:User = {
  id: 2,
  firstName: "Alex",
  lastName: "Rascu",
  email: "alex@gmail.com"
};

const author: Author = {  
  id: 1,
  userId: 2,
  biography: "Decisions: If you can't decide, the answer is no. If two equally difficult paths, choose the one more painful in the short term (pain avoidance is creating an illusion of equality). Choose the path that leaves you more equanimous.",
  country: "Moldova",
  birthDate: "6/8/2024",
  socialMediaLinks : "https://linkedin.com/ https://www.facebook.com/",
  numberOfPosts: 100
};

const sampleAuthor = {
  ...author,
  followers: 100,
  follows: 200,
}
  

const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 800,
  height: 300,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const fabStyle = {
  position: 'absolute',
  bottom: 16,
  right: 16,
};  

const ProfilePage = () => {

  const [bio, setBio] = useState(author.biography);
  const [editMode, setEditMode] = useState(false);
  const [editedBio, setEditedBio] = useState("");

  

  const handleEdit = () => {
    setEditedBio(bio);
    setEditMode(true);
  };

  const handleCloseEditModal = () => {
    setEditMode(false);
  };

  const handleSaveEdit = () => {
    // Perform POST request to update bio with editedBio
    // Upon successful update, update bio state
    setBio(editedBio);
    setEditMode(false);
  };

  const fabs = [
    {
      color: 'primary' as 'primary',
      sx: fabStyle as SxProps,
      icon: <AddIcon />,
      label: 'Add',
    },
    {
      color: 'secondary' as 'secondary',
      sx: fabStyle as SxProps,
      icon: <EditIcon />,
      label: 'Edit',
    },
  ];
  return (
    <Box className="container">
      <Box className="css-t7o3hl">
        {/* Content */}
      </Box>

      <Box className="cardContainer">
        <Card className="card" elevation={24}>
          <CardHeader
            className="cardHeader"
            avatar={
              <Avatar className="avatarContainer" aria-label="recipe">
                <Paper className="avatarPaper" variant="outlined">
                  <img
                    className="avatarImage"
                    src="https://hips.hearstapps.com/hmg-prod/images/william-shakespeare-194895-1-402.jpg"
                    alt="Avatar"
                  />
                </Paper>
              </Avatar>
            }
          />
          <Box sx={{
            width: '90%',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            paddingTop: '80px'
            }}
          >
            <Box 
              component= "div"
              sx={{ 
                width: '60%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column',
              
              }}
            >
              <Grid container  sx={{padding: '5px', width: '100%', height: '100%'}}>

                <Grid item xs={6}  md={9}>
                  <Typography className="grid-overall-num" align="left" variant="h4"  >
                    {user.firstName + " " + user.lastName}
                  </Typography>
                </Grid>
                <Grid item xs={6} md={3}>
                 <Button style={{outline: 'none', borderRadius: 15}} variant="outlined">FOLLOW</Button>
                </Grid>
                  
                <Grid container sx={{ paddingTop: '15px'}} rowSpacing={1} columnSpacing={1}>
                  <Grid item xs={6}  md="auto" >
                    <Typography className="grid-overall-num" component={'span'} align="center"  >
                      {sampleAuthor.numberOfPosts + " "}
                    </Typography>
                    <Typography className="bio-paragraph" component={'span'} align="center"  >
                      Posts
                    </Typography>
                  </Grid>
                  <Grid item xs={6}  md="auto">
                    <Typography className="grid-overall-num" component={'span'} align="center"  >
                      {sampleAuthor.followers + " "}  
                    </Typography>
                    <Typography className="bio-paragraph" component={'span'} align="center"  >
                      Followers
                    </Typography>
                  </Grid>
                  <Grid item xs={6}  md="auto">
                    <Typography className="grid-overall-num" component={'span'} align="center"  >
                      {sampleAuthor.follows + " "} 
                    </Typography>
                    <Typography className="bio-paragraph" component={'span'} align="center"  >
                    Following
                    </Typography>
                  </Grid>
                </Grid>
                <Typography variant="h6" className="bio-paragraph">
                    {bio}
                </Typography>
              </Grid>
            </Box> 
          </Box>
          
          <Box sx={{
            width: '75%',
            paddingTop: '8rem'
            }}
          >
            <Typography className="grid-overall-num" variant="h4">
              Personal Products
            </Typography>
            <Grid sx={{paddingTop: '30px'}} container rowSpacing={4} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
              {dummyArtworks.map((artwork: Artwork) => (
                    <Grid item xs={12} sm={6} md={4} lg={3} key={artwork.id}>                    
                      <ProfileCard artwork={artwork} />             
                    </Grid>
                ))}          
            </Grid>
          </Box> 
        </Card>
      </Box>
      <Modal
        open={editMode}
        onClose={handleCloseEditModal}
        aria-labelledby="edit-bio-modal-title"
        aria-describedby="edit-bio-modal-description"
      >
        <Box className="edit-modal">
          <Box sx={style}>
            <TextField
                    id="modal-bio-textfield"
                    multiline
                    label = "Biography"
                    value={editedBio}
                    onChange={(e) => setEditedBio(e.target.value)}
                    rows={10}
                    sx={{ width: '100%'}}
            />
            <IconButton
              aria-label="close"
              onClick={handleCloseEditModal}
              sx={{
                  position: 'absolute',
                  right: 8,
                  top: 8,
                  color: (theme) => theme.palette.grey[500],
                  outline: 'none',
              }}
              >
              <CloseIcon />
            </IconButton>
          </Box>
        </Box>
      </Modal>
      <Fab color="secondary" aria-label="edit" sx={{ position: 'fixed', bottom: 30, right: 30 }}>
        <EditIcon />
      </Fab>
    </Box>
  );
}

export default ProfilePage;



