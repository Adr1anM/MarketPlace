import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Container, Typography, Box, Paper, Grid } from '@mui/material';
import useArtworks from '../../zsm/stores/useArtworks';
import { Artwork } from '../../types/types';

const ProductDetailPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [artwork, setArtwork] = useState<Artwork | null>(null);
    const artworksStore = useArtworks();

    useEffect(() => {
        const fetchArtwork = async () => {
            if (id) {
                const artworkData = await artworksStore.fetchArtworkById(parseInt(id));
                setArtwork(artworkData);
            }
        };

        fetchArtwork();
    }, [id]);
    console.log("detailed artwork",artwork);

    if (!artwork) {
        return <Typography>Loading...</Typography>;
    }

    console.log("atuhroId from DetailedPage",artwork.authorId);
    return (
        <Container>
            <Grid container spacing={4} alignItems="center" sx={{ marginTop: '3rem' }}>
                <Grid item xs={12} md={6}>
                    <Paper
                        elevation={16}
                        sx={{
                            height: '300px',
                            width: '60%',
                            borderRadius: '15px',
                            backgroundImage: `url(data:image/jpeg;base64,${artwork.imageData})`,
                            backgroundSize: 'cover',
                            backgroundPosition: 'center',
                            marginBottom: '3rem'    
                        }}
                    />
                </Grid>
                <Grid item xs={12} md={6}>
                    <Box>
                        <Typography variant="h4" component="h1" gutterBottom>
                            {artwork.title}
                        </Typography>
                        <Typography variant="h6" component="h2">
                            {artwork.firstName} {artwork.lastName}
                        </Typography>
                        <Typography variant="body1" component="p">
                            {artwork.description}
                        </Typography>
                        <Typography variant="body1" component="p">
                            Price: {artwork.price}$
                        </Typography>
                        <Typography variant="body1" component="p">
                            Created on: {new Date(artwork.createdDate).toLocaleDateString()}
                        </Typography>
                    </Box>
                </Grid>
            </Grid>
        </Container>
    );
};

export default ProductDetailPage;
