import { Avatar, Card, CardActions, CardContent, CardHeader, CardMedia, Collapse, IconButton, Typography } from '@mui/material';
import { red } from '@mui/material/colors';
import React from 'react';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';

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

const ArtworkCard: React.FC<ArtworkCardProps> = ({ artwork }) => {

  const [expanded, setExpanded] = React.useState(false);
  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <Card  sx={{ maxWidth: 345 }}>
      <CardHeader id={artwork.id}
        avatar={
          <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
            R
          </Avatar>
        }
        title={artwork.title} 
      />
       <CardMedia
        component="img"
        height="194"
        image="https://images.joseartgallery.com/100736/what-kind-of-art-is-popular-right-now.jpg"
        alt="Paella dish"
      />
      <CardContent>
        <Typography noWrap variant="body2" color="text.secondary">
          {artwork.firstName + " " + artwork.lastName}
        </Typography>
        <Typography noWrap variant="body2" color="text.secondary">
          {artwork.description}
        </Typography>
      </CardContent>
        <CardContent>
          <Typography paragraph>Price: ${artwork.price}</Typography>
        </CardContent>
    </Card>
  );
};

export default ArtworkCard;
