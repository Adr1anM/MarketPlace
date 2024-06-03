import { Avatar, Card, CardActions, CardContent, CardHeader, Collapse, IconButton, Typography } from '@mui/material';
import { red } from '@mui/material/colors';
import React from 'react';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

interface ArtistsProps{
  artist: {
    id: number;
    userId: number;
    biography: string;
    country: string;
    birthDate: string;
    socialMediaLinks : string;
    numberOfPosts: number;
  };
}

const ArtistCard: React.FC<ArtistsProps> = ({ artist }) => {

  const [expanded, setExpanded] = React.useState(false);
  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card  sx={{ maxWidth: 345 }}>
      <CardHeader id={artist.id}
        avatar={
          <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
            R
          </Avatar>
        }
        title={artist.numberOfPosts}
        subheader={new Date(artist.birthDate).toLocaleDateString()}
      />
      <CardContent>
        <Typography noWrap variant="body2" color="text.secondary">
          {artist.biography}
        </Typography>
      </CardContent>
      <CardActions disableSpacing>
        <IconButton aria-label="show more" onClick={handleExpandClick}>
          <ExpandMoreIcon />
        </IconButton>
      </CardActions>
      <Collapse in={expanded} timeout="auto" unmountOnExit>
        <CardContent>
          <Typography paragraph>Links: {artist.socialMediaLinks}</Typography>
        </CardContent>
      </Collapse>
    </Card>
  );
};

export default ArtistCard;
