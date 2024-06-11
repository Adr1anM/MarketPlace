import { Box, Button, Card, Container, CssBaseline, Divider, Drawer, IconButton, IconButtonProps, List, ListItem, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import {Artwork} from '../../types/types'
import { styled } from '@mui/material/styles';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import ArtworkCard from '../cards/ArtworkCard';
import { dummyArtworks } from '../../dummyDataStore/artworksData';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import React from 'react';
import FilterListIcon from '@mui/icons-material/FilterList';
import './pagesStyles/ArtworksPage.css'

const ContentContainer = styled(Container)({
   margin: "2rem 0"
  });


const buttonStyle = {
    color: 'black',
    outline: 'none',
    backgroundColor: 'white',
    boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.1)',
    transition: 'box-shadow 0.3s ease-in-out',
    '&:hover': {
        backgroundColor: 'white',
        outline: 'none',
        boxShadow: '0px 4px 12px rgba(0, 0, 0, 0.15)',
    },
};



const ArtworksPage = () =>{
    
    const [open, setOpen] = React.useState(false);

    const toggleDrawer = (newOpen: boolean) => () => {
        setOpen(newOpen);
    };

    
  const DrawerList = (
    <Box sx={{ width: 250 }} role="presentation" onClick={toggleDrawer(false)}>
      <List>
        {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
          <ListItem key={text} disablePadding>
            <ListItemButton>
              <ListItemIcon>
                {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
              </ListItemIcon>
              <ListItemText primary={text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        {['All mail', 'Trash', 'Spam'].map((text, index) => (
          <ListItem key={text} disablePadding>
            <ListItemButton>
              <ListItemIcon>
                {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
              </ListItemIcon>
              <ListItemText primary={text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Box>
  );
    
    return(
        <>
            <CssBaseline />
            <Container maxWidth={false}
                       sx={{ bgcolor: 'gray',
                         width: '100%',
                         height: '100%',
                         border: 'InactiveCaption',
                         display: 'flex', 
                         flexDirection: 'column', 
                         alignItems: 'center',  
                         justifyContent: 'center', 
                       }}>

             <h1>Collect art and design online</h1>

            <div>
            <Button  sx={buttonStyle} startIcon = {<FilterListIcon />} onClick={toggleDrawer(true)}>All Filters</Button>
            <Drawer open={open} onClose={toggleDrawer(false)}>
                {DrawerList}
            </Drawer>
            </div>           
            <ContentContainer>
                <Grid container rowSpacing={2} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
                    {dummyArtworks.map((artwork: Artwork) => (
                        <Grid item xs={12} sm={6} md={4} lg={3} key={artwork.id}>                    
                          <ArtworkCard artwork={artwork} />             
                        </Grid>
                    ))}     
                </Grid>
            </ContentContainer>
            </Container>
        </>
    );  
}


export default ArtworksPage;