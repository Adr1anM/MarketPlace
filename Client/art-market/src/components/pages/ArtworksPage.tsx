import React, { useState, useEffect } from 'react';
import { Container, Grid, Button, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Divider, Box } from '@mui/material';
import { styled } from '@mui/material/styles';
import FilterListIcon from '@mui/icons-material/FilterList';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import ArtworkCard from './profile/ArtworkCard';
import { useAuth } from '../../contexts/AuthContext';
import useArtworks from '../../zsm/stores/useArtworks';
import { PagedRequest } from '../../types/types';



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

const ArtworksPage = () => {
    const { isLoggedIn, user } = useAuth();
    const artworksStore = useArtworks();
    const [open, setOpen] = useState(false);
    const [paginationState, setPaginationState] = useState<PagedRequest>({
        pageIndex: 0,
        pageSize: 15,
        columnNameForSorting: "Price",
        sortDirection: "asc",
        requestFilters: {
          logicalOperator: 0,
          filters: [
            {
            path: "Price",
            value: "500",
            operator: "gt"
            }
          ]
        }
});

  console.log(artworksStore.pagedArtworks);
    useEffect(() => {
        const fetchArtworks = async () => {
            await artworksStore.fetchPagedArtworks(paginationState);
        };

        fetchArtworks();
    }, [paginationState]);

    function handleDelete(artworkId: number): void {
        throw new Error('Function not implemented.');
    }

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

    return (
        <>
            <Container maxWidth={false} sx={{
                bgcolor: 'lightgray',
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
                    <Button sx={buttonStyle} startIcon={<FilterListIcon />} onClick={toggleDrawer(true)}>All Filters</Button>
                    <Drawer open={open} onClose={toggleDrawer(false)}>
                        {DrawerList}
                    </Drawer>
                </div>
                <ContentContainer>
                    <Grid container rowSpacing={2} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
                        {artworksStore.pagedArtworks?.artworks.map((artwork) => (
                            <Grid item xs={12} sm={6} md={4} lg={3} key={artwork.id}>
                                <ArtworkCard artwork={artwork} onDelete={handleDelete} user={user} />
                            </Grid>
                        ))}
                    </Grid>
                </ContentContainer>
            </Container>
        </>
    );
}

export default ArtworksPage;
