import { Box, Card, Container, CssBaseline, IconButton, IconButtonProps } from '@mui/material';
import {Artwork} from '../../types/types'
import * as React from 'react';
import { styled } from '@mui/material/styles';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import ArtworkCard from '../cards/ArtworkCard';
import { dummyArtworks } from '../../dummyDataStore/artworksData';

const ContentContainer = styled(Container)({
   margin: "2rem 0"
  });

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  ...theme.typography.body2,
  padding: theme.spacing(1),
  textAlign: 'center',
  color: theme.palette.text.secondary,
}));



const ArtworksPage = () =>{

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
            <ContentContainer>
                <Grid container rowSpacing={2} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
                    {dummyArtworks.map((artwork: Artwork) => (
                        <Grid item xs={12} sm={6} md={4} lg={3} key={artwork.id}>
                        <Item>
                            <ArtworkCard artwork={artwork} />
                        </Item>
                        </Grid>
                    ))}     
                </Grid>
            </ContentContainer>
            </Container>
        </>
    );  
}


export default ArtworksPage;