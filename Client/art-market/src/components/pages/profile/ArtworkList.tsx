import { Box, Grid, Typography } from "@mui/material";
import { memo } from "react";
import { Artwork, User } from "../../../types/types";
import '../pagesStyles/ProfilePage.css';
import ArtworkCard from "./ArtworkCard";

interface ArtworkStoreType {
    artworks: Artwork[];
    // Add any other properties or methods from the artworksStore object
  }
  
  interface ArtworkListProps {
    artworksStore: ArtworkStoreType;
    handleDelete: (artworkId: number) => void;
    user: User | undefined;
  }


const ArtworkList = memo<ArtworkListProps>(({ artworksStore, handleDelete, user }) => {

    return (
      <Box sx={{ width: '75%', paddingTop: '8rem' }}>
        <Typography variant="h4">Personal Products</Typography>
        <Grid sx={{ paddingTop: '30px' }} container rowSpacing={4} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
          {artworksStore.artworks.map((artwork) => (
            <Grid item xs={12} sm={6} md={4} lg={3} key={artwork.id}>
              <ArtworkCard artwork={artwork}  onDelete={handleDelete} user={user} />
            </Grid>
          ))}
        </Grid>
      </Box>
    );
});


export default ArtworkList;